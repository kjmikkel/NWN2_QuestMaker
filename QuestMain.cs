/* 
 * This file is part of QuestMaker.
 * QuestMaker is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * QuestMaker is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU Lesser Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser Public License
 * along with QuestMaker.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Collections;

using NWN2Toolset.NWN2.Views;
using NWN2Toolset.NWN2.Data.TypedCollections;
using NWN2Toolset.NWN2.Data.Journal;
using NWN2Toolset.Plugins;
using NWN2Toolset.NWN2.Data;
using NWN2Toolset.NWN2.Data.Blueprints;
using NWN2Toolset.NWN2.Data.Campaign;
using NWN2Toolset.NWN2.Data.Instances;
using NWN2Toolset.NWN2.Data.Templates;

using TD.SandBar;

using OEIShared.Utils;
using OEIShared.IO;
using OEIShared.IO.TwoDA;
using OEIShared.UI;
using OEIShared.UI.ImageLoader;
using NWN2Toolset;


namespace QuestMaker
    {
    /// <summary>
    /// Description of QuestMain.
    /// </summary>
    public partial class QuestMain : Form
        {

        private Actor giver;
        private Actor villian;

        private LinkedList<Actor> extra;
        private LinkedList<Actor> props;
        private LinkedList<Actor> triggers;

        private NWN2GameModule module;

        private const ushort ICON_RESOURCE_TYPE = 3;

        private BWLanguages.Gender gender;
        private static getBlueprint getBlueprintForm;
        private static ModifyNew modifyForm;
        private BWLanguages.BWLanguage lang;
        private NWN2JournalPriority pri;
        private static TwoDAColumnCollection nwn2IconsColumnCollection;
        private static TwoDAFile nwn2IconsFile;
        private String filePath;
        String filter = "Quest files (*.qus)|*.qus";

        /// <summary>
        /// The constructor for the form
        /// </summary>
        /// <param name="filePath">The path to where quests must be saved to and loaded from</param>
        public QuestMain(ref String filePath)
            {
            InitializeComponent();

            this.filePath = filePath;
            extra = new LinkedList<Actor>();
            props = new LinkedList<Actor>();
            triggers = new LinkedList<Actor>();
            debug("Plug-Started");

            comboPri.SelectedIndex = 0;
            comboLang.SelectedIndex = 0;

            this.module = NWN2Toolset.NWN2ToolsetMainForm.App.Module;
            genderBox.SelectedIndex = 0;

            if (nwn2IconsFile == null)
                {
                nwn2IconsFile = TwoDAManager.Instance.Get("nwn2_icons");
                nwn2IconsColumnCollection = nwn2IconsFile.Columns;
                }
            }

        #region Move
        /// <summary>
        /// Moves the selected Story Node one place up
        /// </summary>
        private void MoveUp(object sender, EventArgs e)
            {
            if (sNodeList.SelectedIndices[0] > 0)
                {
                Object firstToMove;

                // I get the item above the selected object
                int selectedIndex = sNodeList.SelectedIndices[0] - 1;
                /* Since we remove the item, we need to put it where the currently
                last selected item is*/
                int moveTo = sNodeList.SelectedIndices[sNodeList.SelectedItems.Count - 1];

                firstToMove = sNodeList.Items[selectedIndex];
                sNodeList.Items.Remove(firstToMove);
                sNodeList.Items.Insert(moveTo, firstToMove);
                int last = sNodeList.SelectedItems.Count - 1;
                moveDown.Enabled = sNodeList.SelectedIndices[last] < sNodeList.Items.Count - 1;
                MoveUpButton.Enabled = sNodeList.SelectedIndices[0] > 0;
            }
            }

        /// <summary>
        /// Moves the selected Story Node one place down
        /// </summary>
        private void MoveDown(object sender, EventArgs e)
            {
            int last = sNodeList.SelectedItems.Count - 1;
            if (sNodeList.SelectedIndices[last] != sNodeList.Items.Count - 1)
                {
                object temp;
                // I get the item above the selected object
                int selectedIndex = sNodeList.SelectedIndices[last] + 1;
                /* Since we remove the item, we need to put it where the currently
                first item is*/
                int moveTo = sNodeList.SelectedIndices[0];

                temp = sNodeList.Items[selectedIndex];
                sNodeList.Items.Remove(temp);
                sNodeList.Items.Insert(moveTo, temp);

                moveDown.Enabled = sNodeList.SelectedIndices[last] < sNodeList.Items.Count - 1;
                MoveUpButton.Enabled = sNodeList.SelectedIndices[0] > 0;
                }
            }
        #endregion

        #region sNodeList
        /// <summary>
        /// Should find all the previous elements in the Story Node list
        /// </summary>
        /// <param name="objs">The elements we are looking through</param>
        /// <param name="sNode">The node we are search until</param>
        private LinkedList<StoryNode> getPrev(ListBox.ObjectCollection objs, StoryNode sNode)
            {
            LinkedList<StoryNode> prevStoryNodes = new LinkedList<StoryNode>();
            StoryNode tempNode;
            foreach (object prevsNode in objs)
                {
                tempNode = (StoryNode)prevsNode;
                // If the StoryNode has either a journal entry or is an endPoint, then we skip it
                if (sNode != null && sNode == tempNode)
                    break;
                if (!tempNode.journalCheck || tempNode.endPoint) continue;
                prevStoryNodes.AddLast(tempNode);
                }
            return prevStoryNodes;
            }

        #region StoryNode
        /// <summary>
        /// Adds a story node that has the ending part of the trigger node that has just been added
        /// </summary>
        /// <param name="tempSNode">The story node to which we must add a  corosponding Trigger end node</param>
        private void addTriggerStoryNode(StoryNode tempSNode)
            {
            StoryNode newStoryNode = new StoryNode(tempSNode.Name + " end ", tempSNode.actor, tempSNode.trigger, module);
            newStoryNode.happensEnum = EnumTypes.happens.Trigger;
            newStoryNode.triggerHappens = tempSNode.convHappens;
            newStoryNode.preReqNode = tempSNode;
            newStoryNode.preReq = EnumTypes.prereq.SimplePrereq;
            if (tempSNode.convHappens == StoryNode.convType.Escort)
                {
                newStoryNode.greeting = "Thank you for Escorting me.";
                newStoryNode.journal = "I took him to his destination.";
                }
            else if (tempSNode.convHappens == StoryNode.convType.Explore)
                {
                newStoryNode.greeting = "XP granted for exploration";
                newStoryNode.journal = "I found the place I was looking for";
                }
            newStoryNode.endPoint = true;
            sNodeList.Items.Add(newStoryNode);
            }

        /// <summary>
        /// Creates a new story node
        /// </summary>
        private void newStoryNode(object sender, EventArgs e)
            {
            LinkedList<StoryNode> prevStoryNodes = getPrev(sNodeList.Items, null);

            StoryNode tempSNode;
            StoryNodeForm form = new StoryNodeForm(giver, villian, extra, props, prevStoryNodes, triggers, module);
            if (form.ShowDialog() == DialogResult.OK)
                {
                tempSNode = form.getStoryNode();
                sNodeList.Items.Add(tempSNode);

                if (tempSNode.convHappens != StoryNode.convType.None)
                    {
                    addTriggerStoryNode(tempSNode);
                    }
                }

            }

        /// <summary>
        /// Edits the selected story node
        /// </summary>
        private void editStoryNode(object sender, EventArgs e)
            {
            StoryNode sNode = (StoryNode)sNodeList.SelectedItem;

            int index = sNodeList.SelectedIndex;

            // I get all story nodes before the selected one
            LinkedList<StoryNode> prevStoryNodes = getPrev(sNodeList.Items, sNode);

            StoryNodeForm form = new StoryNodeForm(sNode, giver, villian, extra, props, prevStoryNodes, triggers, module);
            StoryNode tempStoryNode;
            if (form.ShowDialog() == DialogResult.OK)
                {
           //     int indexOfOldsNode = sNodeList.Items.IndexOf(sNode);
                debug("The number is: " + index);   
                tempStoryNode = form.getStoryNode();
                sNodeList.Items[index] = tempStoryNode;
                }
            }
        #endregion

        /// <summary>
        /// Removes a story node
        /// </summary>
        private void removesNodeButton_Click(object sender, EventArgs e)
            {
            sNodeList.Items.Remove(sNodeList.SelectedItem);
            }
        #endregion

        #region Browse
        /// <summary>
        /// Lets the user browse for blueprints to the giver actor position
        /// </summary>
        private void giverBrowse_Click(object sender, EventArgs e)
            {
            giver = genericBrowse(GiverGrid, giverEdit, null);
            if (GiverGrid.RowCount > 0 && (String)GiverGrid[0, 0].Value != String.Empty)
                {
                newsNodeButton.Enabled = true;
                }
            }


        /// <summary>
        /// Lets the user browse for blueprints to the villian actor position
        /// </summary>
        private void villianBrowse_Click(object sender, EventArgs e)
            {
            villian = genericBrowse(VillianGrid, villianEdit, villianRemove);
            }

        /// <summary>
        /// Lets the user browse for blueprints to the extra actors position
        /// </summary>
        private void extraBrowse_Click(object sender, EventArgs e)
            {
            genericBrowseMultiple(ExtraGrid, EnumTypes.actorType.Creature | EnumTypes.actorType.Door | EnumTypes.actorType.Placeable, ref extra, "extraData");
            }

        /// <summary>
        /// Lets the user browse for blueprints to the triggers
        /// </summary>
        private void getTriggerRegions(object sender, EventArgs e)
            {
            genericBrowseMultiple(triggerRegionsGrid, EnumTypes.actorType.TriggerRegion, ref triggers, "triggerData");
            }

        /// <summary>
        /// Lets the user browse for blueprints to the items
        /// </summary>
        private void propBrowse_Click(object sender, EventArgs e)
            {
            genericBrowseMultiple(PropGrid, EnumTypes.actorType.Item, ref props, "propData");
            }

        #endregion

        #region getBlueprint
        /// <summary>
        /// The "gasket" method that is used by all the methods that can have multiple actors 
        /// In other words: extras, items and triggers
        /// </summary>
        /// <param name="grid">The grid where the actors must be added</param>
        /// <param name="editButton">The edit button that must be enabled, if there at the end of the method
        /// is at least one element in the grid</param>
        /// <param name="removeButton">Ditto for the remove button</param>
        /// <param name="type">The type of the actor we are looking for</param>
        /// <param name="actorList">The list of actors that we are working with</param>
        /// <param name="dataCell">The name of the cell in the grid that stores the actual blueprint 
        /// of the actors we extract</param>
        private static void genericBrowseMultiple(DataGridView grid, 
    EnumTypes.actorType type, ref LinkedList<Actor> actorList, String dataCell)
            {
            getBlueprint getBlueprintForm = getBlueprint.getBlue(type);
            if (getBlueprintForm.ShowDialog() == DialogResult.OK)
                {
                LinkedList<Actor> actors = getBlueprintForm.getBlueprints();
                setGrid(grid, actors, actorList, dataCell, type);
                }
            }

        /// <summary>
        /// The "gasket" method that is used by all the methods that can have only one actor 
        /// In other words: giver and villian
        /// </summary>
        /// <param name="grid">The grid where the actor must be added</param>
        /// <param name="editButton">The edit button that must be enabled, if there at the end of the method
        /// is one element in the grid</param>
        /// <param name="removeButton">Ditto for the remove button</param>
        private Actor genericBrowse(DataGridView grid, Button editButton, Button removeButton)
            {
            Actor actor = null;
            getBlueprintForm = getBlueprint.getBlue(EnumTypes.actorType.Creature | EnumTypes.actorType.Door | EnumTypes.actorType.Placeable);
            if (getBlueprintForm.ShowDialog() == DialogResult.OK)
                {
             
                grid.RowCount = 1;
                LinkedList<Actor> prints = getBlueprintForm.getBlueprints();
                if (prints.Count > 0)
                    {
                    actor = prints.First.Value;
                    grid[0, 0].Value = actor.ToString();
                    grid[1, 0].Value = actor.Tag;
                    grid[2, 0].Value = actor;
                    editButton.Enabled = true;
                    if (removeButton != null)
                        removeButton.Enabled = true;
                    }
                }
            return actor;
            }

        #endregion
        /// <summary>
        /// Makes sure that the correct buttons are enabled after having changed the
        /// content of the list of Story Nodes in the current quest
        /// </summary>
        private void sNodeList_SelectedValueChanged(object sender, EventArgs e)
            {
            bool sNodeEntry = (sNodeList.Items.Count > 0);
            editsNodeButton.Enabled = sNodeEntry;
            removesNodeButton.Enabled = sNodeEntry;
            int last = sNodeList.SelectedItems.Count - 1;
            debug("number: " + last);
            if (last > 0)
                {
                moveDown.Enabled = sNodeEntry && sNodeList.SelectedIndices[last] < sNodeList.Items.Count - 1;
                MoveUpButton.Enabled = sNodeEntry && sNodeList.SelectedIndices[0] > 0;
                }
            else
                {
                moveDown.Enabled = false;
                MoveUpButton.Enabled = false;
                }
                }

        /// <summary>
        /// Removes the current villian
        /// </summary>
        private void villianRemove_Click(object sender, EventArgs e)
            {
            villian = null;
            VillianGrid[0, 0].Value = null;
            VillianGrid[1, 0].Value = null;
            VillianGrid[2, 0].Value = null;
            villianEdit.Enabled = false;
            villianRemove.Enabled = false;
            }

        /// <summary>
        /// Removes the currently selected extra
        /// </summary>
        private void extraRemove_Click(object sender, EventArgs e)
            {
            remove(ExtraGrid, extraEdit, extraRemove, ref extra, "extraData");
            }

        /// <summary>
        /// Tries to create the quest
        /// </summary>
        private void Done_Click(object sender, EventArgs e)
            {
            LinkedList<StoryNode> ApprovedStoryNodes = new LinkedList<StoryNode>();
            /* I check to see if there are any illogical dependencies (that is
             * if a node requires something that is not there, or has been moved to a later position
             */
            int dependencyNumber = 0;

            pri = (NWN2JournalPriority)comboPri.SelectedIndex;
            lang = (BWLanguages.BWLanguage)comboLang.SelectedIndex;

            if (questName.Text == String.Empty)
                {
                report("You need to give the Quest a name");
                return;
                }

            foreach (StoryNode sNode in sNodeList.Items)
                {
                sNode.questId = (int)dependencyNumber;
                dependencyNumber += 5;
                ApprovedStoryNodes.AddLast(sNode);

                if (sNode.preReqNode != null)
                    {
                    if (!ApprovedStoryNodes.Contains(sNode.preReqNode))
                        {
                        report("There is a problematic dependency in " + sNode.Name + "\nPlease fix this and try again");
                        return;
                        }
                    }
                }

            string name = questName.Text;
            NWN2JournalCategory category = null;
            var actorMap = new Hashtable();
            var triggerMap = new Hashtable();
            Actor actor = null;
            Actor trigger = null;

            StoryNode.wipeConvHash();

            foreach (StoryNode sNode in ApprovedStoryNodes)
                {
                // This is done in order to ensure that the newest version of an actor is allways allocated
                actor = (Actor)actorMap[sNode.actor.Tag];
                if (actor != null)
                    sNode.actor = actor;

                if (sNode.trigger != null)
                    trigger = (Actor)triggerMap[sNode.trigger.Tag];

                if (trigger != null)
                    sNode.trigger = trigger;

                sNode.addConv(name, gender, lang);

                // I add the newest version of the actors
                actorMap[sNode.actor.Tag] = sNode.actor;

                if (sNode.trigger != null)
                    triggerMap[sNode.trigger.Tag] = sNode.trigger;

                if (sNode.journalCheck)
                    {
                    // I create the journal
                    category = sNode.createJournal(name, pri, gender, lang, category);
                    }
                }

            #region old code - might want to cut
            /*
            NWN2Campaign activeCampaign = NWN2CampaignManager.Instance.ActiveCampaign;
            foreach (Object actObj in actorMap.Values)
                {
                var act = (Actor)actObj;
                switch (act.type)
                    {
                    case EnumTypes.actorType.Creature:
                        toolsetCreatureUpdate(act, activeCampaign);
                        break;

                    case EnumTypes.actorType.Door:
                        toolsetDoorUpdate(act, activeCampaign);
                        break;

                    case EnumTypes.actorType.Placeable:
                        toolsetPlaceableUpdate(act, activeCampaign);
                        break;
                    }
                }

            foreach (Object trigObj in triggerMap.Values)
                {
                var trigAct = (Actor)trigObj;
                if (trigAct.boolInstance)
                    {
                    var instTrigger = (NWN2TriggerInstance)trigAct.instance;
                    foreach (NWN2GameArea area in module.Areas.Values)
                        {
                        area.Demand();
                        foreach (NWN2TriggerInstance trig in area.Triggers)
                            {
                            if (trig.Tag == trigAct.Tag)
                                {
                                debug(trigAct.Tag);
                                area.Triggers.Remove(trig);
                                area.Triggers.Add(trigAct.instance);
                                
                                trig.Variables = instTrigger.Variables;
                                trig.OnEnter = instTrigger.OnEnter;
                                
                                  }
                            }
                        }
                    }
                else
                    {
                    setCampaignModuleValues((NWN2TriggerBlueprint)trigAct.blueprint, module.Placeables);
                    setCampaignModuleValues((NWN2TriggerBlueprint)trigAct.blueprint, activeCampaign.Placeables);
                    }
                }
            }

        void toolsetCreatureUpdate(Actor act, NWN2Campaign activeCampaign)
            {
            if (act.boolInstance)
                {
                var instCreature = (NWN2CreatureInstance)act.instance;
                foreach (NWN2GameArea area in module.Areas.Values)
                    {
                    area.Demand();
                    foreach (NWN2CreatureInstance creature in area.Creatures)
                        if (creature.Tag == instCreature.Tag)
                            {
                            creature.Conversation = instCreature.Conversation;
                            creature.Inventory = instCreature.Inventory;
                            }
                    }
                }
            else
                {
                setCampaignModuleValues((NWN2CreatureBlueprint)act.blueprint, module.Creatures);
                setCampaignModuleValues((NWN2CreatureBlueprint)act.blueprint, activeCampaign.Creatures);
                }
            */
            }
        /*
           void toolsetDoorUpdate(Actor act, NWN2Campaign activeCampaign)
               {
               if (act.boolInstance)
                   {
                   var instDoor = (NWN2DoorInstance)act.instance;
                   foreach (NWN2GameArea area in module.Areas.Values)
                       {
                       area.Demand();
                       foreach (NWN2DoorInstance door in area.Doors)
                           {
                           if (door.Tag == instDoor.Tag)
                               {
                               door.OnFailToOpen = instDoor.OnFailToOpen;
                               door.Locked = instDoor.Locked;
                               door.Conversation = instDoor.Conversation;
                               door.OnOpen = instDoor.OnOpen;
                               door.OnDeath = instDoor.OnDeath;
                               door.Variables = instDoor.Variables;
                               }
                           }
                       }
                   }
               else
                   {
                   setCampaignModuleValues((NWN2DoorBlueprint)act.blueprint, module.Doors);
                   setCampaignModuleValues((NWN2DoorBlueprint)act.blueprint, activeCampaign.Creatures);
                   }
               }

           void toolsetPlaceableUpdate(Actor act, NWN2Campaign activeCampaign)
               {
               if (act.boolInstance)
                   {
                   var instPlac = (NWN2PlaceableInstance)act.instance;
                   foreach (NWN2GameArea area in module.Areas.Values)
                       {
                       area.Demand();
                       foreach (NWN2PlaceableInstance placeable in area.Placeables)
                           {
                           if (placeable.Tag == instPlac.Tag)
                               {
                               placeable.Conversation = instPlac.Conversation;
                               placeable.Inventory = instPlac.Inventory;
                               placeable.Conversation = instPlac.Conversation;
                               placeable.OnUsed = instPlac.OnUsed;
                               placeable.Static = instPlac.Static;
                               placeable.Usable = instPlac.Usable;
                               placeable.HasInventory = instPlac.HasInventory;

                               placeable.Variables = instPlac.Variables;
                               placeable.OnDeath = instPlac.OnDeath;
                               }
                           }
                       }
                   }
               else
                   {
                   setCampaignModuleValues((NWN2PlaceableBlueprint)act.blueprint, module.Placeables);
                   setCampaignModuleValues((NWN2PlaceableBlueprint)act.blueprint, activeCampaign.Placeables);
                   }
               }
           */
            #endregion
        /// <summary>
        /// Sets the language
        /// </summary>
        private void setLang()
            {
            if (giver != null)
                {
                giver.lang = lang;
                }

            if (villian != null)
                {
                villian.lang = lang;
                }

            foreach (Actor ex in extra)
                {
                ex.lang = lang;
                }

            foreach (Actor pr in props)
                {
                pr.lang = lang;
                }
            }

        #region setCampignModuleInv
        /*
        /// <summary>
        /// Updates the door in the campaign 
        /// </summary>
        /// <param name="door"></param>
        /// <param name="collection"></param>
        private static void setCampaignModuleValues(NWN2DoorBlueprint door, NWN2Toolset.NWN2.Data.TypedCollections.NWN2BlueprintCollection collection)
            {
            var print = (NWN2DoorBlueprint)collection[door.TemplateResRef];
            if (print != null)
                {
                print.OnFailToOpen = door.OnFailToOpen;
                print.Locked = door.Locked;
                print.Conversation = door.Conversation;

                print.Variables = door.Variables;
                print.OnDeath = door.OnDeath;
                print.OnOpen = door.OnOpen;
                }
            }

        private static void setCampaignModuleValues(NWN2PlaceableBlueprint placeable, NWN2Toolset.NWN2.Data.TypedCollections.NWN2BlueprintCollection collection)
            {
            var print = (NWN2PlaceableBlueprint)collection[placeable.TemplateResRef];
            if (print != null)
                {
                print.Conversation = placeable.Conversation;
                print.Inventory = placeable.Inventory;
                print.OnUsed = placeable.OnUsed;
                print.Static = placeable.Static;
                print.Usable = placeable.Usable;
                print.Inventory = placeable.Inventory;
                print.HasInventory = placeable.HasInventory;

                print.Variables = placeable.Variables;
                print.OnDeath = placeable.OnDeath;
                print.OnOpen = placeable.OnOpen;
                }
            }

        private static void setCampaignModuleValues(NWN2TriggerBlueprint trigger, NWN2Toolset.NWN2.Data.TypedCollections.NWN2BlueprintCollection collection)
            {
            var print = (NWN2TriggerBlueprint)collection[trigger.TemplateResRef];
            if (print != null)
                {
                debug("InTrigger");
                collection.Remove(print);
                collection.Add(trigger);
            
                }
            }

        private static void setCampaignModuleValues(NWN2CreatureBlueprint creature, NWN2Toolset.NWN2.Data.TypedCollections.NWN2BlueprintCollection collection)
            {
            var print = (NWN2CreatureBlueprint)collection[creature.TemplateResRef];
            if (print != null)
                {
                System.Console.WriteLine("Module campaignSet");
                print.Conversation = creature.Conversation;
                print.Inventory = creature.Inventory;
                }
            }
        */
        #endregion

        /// <summary>
        /// Is called when the language combobox is changed
        /// </summary>
        private void comboLang_SelectedIndexChanged(object sender, EventArgs e)
            {
            lang = (BWLanguages.BWLanguage)comboLang.SelectedIndex;
            setLang();
            }

        #region Load and Save
        /// <summary>
        /// Saves the current quest
        /// </summary>
        private void save_Click(object sender, EventArgs e)
            {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = filter;
            if (filePath != null && filePath != "")
                save.InitialDirectory = filePath;

            if (save.ShowDialog() == DialogResult.OK)
                {
                String fileName = save.FileName;
                FileInfo info = new FileInfo(fileName);

                Quest quest = new Quest();

                quest.name = questName.Text;
                quest.giver = giver;
                quest.villan = villian;

                //       quest.extras = extra.
                copyToArrayValues(ref quest.extras, extra);
                copyToArrayValues(ref quest.props, props);
                copyToArrayValues(ref quest.triggers, triggers);

                quest.storyNodes = new StoryNode[sNodeList.Items.Count];
                sNodeList.Items.CopyTo(quest.storyNodes, 0);


                FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                var formater = new BinaryFormatter();
                formater.Serialize(fileStream, quest);
                fileStream.Close();
                }
            }

        public void saveFile(object sender, System.EventArgs e)
            {
           
            }

        /// <summary>
        /// Loads a quest
        /// </summary>
        private void load_Click(object sender, EventArgs e)
            {
            OpenFileDialog load = new OpenFileDialog();
            load.Filter = filter;
            if (filePath != null && filePath != "")
                load.InitialDirectory = filePath;

            if (load.ShowDialog() == DialogResult.OK)
                {
                String fileName = load.FileName;
                ResolveEventHandler resolveEventHandler = new ResolveEventHandler(CurrentDomain_AssemblyResolve);
                AppDomain.CurrentDomain.AssemblyResolve += resolveEventHandler;

                FileStream file = new FileStream(fileName, FileMode.Open);
                debug("The file is found at: " + fileName);
                BinaryFormatter bin = new BinaryFormatter();

                Object temp = bin.Deserialize(file);
                Quest quest = temp as Quest;  //bin.Deserialize(file) as Quest;
                file.Close();

                if (quest == null)
                    {
                    report("Quest is null - cannot load plot template");
                    return;
                    }

                // I reset the GridValues
                resetGrid(GiverGrid);
                resetGrid(VillianGrid);
                resetGrid(ExtraGrid);
                resetGrid(PropGrid);
                resetGrid(triggerRegionsGrid);

                questName.Text = quest.name;

                giver = quest.giver;
                villian = quest.villan;

                copyFromArrayValues(quest.extras, ref extra);
                copyFromArrayValues(quest.props, ref props);
                copyFromArrayValues(quest.triggers, ref triggers);

                if (giver != null)
                    {
                    setGrid(GiverGrid, giver);
                    newsNodeButton.Enabled = true;
                    }

                setGrid(VillianGrid, villian);

                setGrid(ExtraGrid, extra, null, "extraData", EnumTypes.actorType.Creature | EnumTypes.actorType.Door | EnumTypes.actorType.Placeable);

                setGrid(PropGrid, props, null, "propData", EnumTypes.actorType.Item);

                setGrid(triggerRegionsGrid, triggers, null, "triggerData", EnumTypes.actorType.TriggerRegion);

                StoryNode.setModule();

                sNodeList.Items.Clear();

                if (quest.storyNodes != null)
                    sNodeList.Items.AddRange(quest.storyNodes);

                newsNodeButton.Enabled = giver != null;
                }
            }

        /// <summary>
        /// Utility method to move the items in a linkedlist to an array
        /// </summary>
        /// <param name="actorArray">The array to coppy the values to</param>
        /// <param name="linkedList">The linked list that contains the actors</param>
        private static void copyToArrayValues(ref Actor[] actorArray, LinkedList<Actor> linkedList)
            {
            if (linkedList.Count > 0)
                {
                actorArray = new Actor[linkedList.Count];
                linkedList.CopyTo(actorArray, 0);
                }
            }

        /// <summary>
        /// Utility method to move the items in a array to a linkedlist
        /// </summary>
        /// <param name="actorArray">The array to move them from</param>
        /// <param name="linkedList">The linkedlist to insert them into</param>
        private static void copyFromArrayValues(Actor[] actorArray, ref LinkedList<Actor> linkedList)
            {
            if (actorArray != null && actorArray[0] != null)
                linkedList = new LinkedList<Actor>(actorArray);
            else
                linkedList = new LinkedList<Actor>();
            }
        #endregion

        /// <summary>
        /// Adds an actor to a grid
        /// </summary>
        /// <param name="grid">The grid that the actor will be added to</param>
        /// <param name="actor">The actor to add to the grid</param>
        private void setGrid(DataGridView grid, Actor actor)
            {
            if (actor != null)
                {
                if (grid.RowCount < 1) grid.RowCount = 1;
                grid[0, 0].Value = actor.ToString();
                grid[1, 0].Value = actor.Tag;
                grid[2, 0].Value = actor;
                }
            }

        /// <summary>
        /// Inserts the actors into the choosen grid, as well as adding them to the quests list of 
        /// </summary>
        /// <param name="grid">The grid where the actors are inserted</param>
        /// <param name="actors">The list of actors which we are going to insert</param>
        /// <param name="actorList">The list of actors that the system is going to add the new actors to</param>
        /// <param name="dataCell">The cell in the grid where the actor object is going to be stored</param>
        /// <param name="type">The type of the actors</param>
        private static void setGrid(DataGridView grid, LinkedList<Actor> actors, LinkedList<Actor> actorList, String dataCell, EnumTypes.actorType type)
            {
            int i = grid.RowCount;
            if (actors != null)
                {
                foreach (Actor actor in actors)
                    {
                    debug("Got actor");
                    if ((actor.type & type) == actor.type)
                        {
                        debug("set value in grid");
                        grid.RowCount++;
                        grid[0, i].Value = actor.ToString();
                        grid[1, i].Value = actor.Tag;

                        if (actorList != null)
                            actorList.AddLast(actor);

                        grid[dataCell, i].Value = actor;

                        debug("Starting image");
                        if (type == EnumTypes.actorType.Item)
                            fixImage(actor, grid, i);
                        i++;
                        }
                    }
                }
            }

        /// <summary>
        /// Inserts an image of the item contained in the actor to the grid in the correct cell
        /// </summary>
        /// <param name="actor">The actor (which will of the type item) that contains the referenes to the icon that will be used</param>
        /// <param name="grid">The grid where the image is inserted in</param>
        /// <param name="index">The index where to incert the image</param>
        private static void fixImage(Actor actor, DataGridView grid, int index)
            {
            String imageFilename = String.Empty;
            TwoDAReference baseItem2daRef = actor.BaseItem;
            Bitmap itemIcon = System.Drawing.SystemIcons.Question.ToBitmap();
            TwoDAReference icon2daRef = actor.Icon;
            TwoDAColumn iconColumn = nwn2IconsColumnCollection["ICON"];
            if (icon2daRef != null && iconColumn != null && iconColumn.IsPopulatedValue(icon2daRef.Row))
                {
                imageFilename = iconColumn.LiteralValue(icon2daRef.Row);
                OEIResRef oeiResRef = new OEIResRef(imageFilename);
                IResourceEntry imageResource = ResourceManager.Instance.GetEntry(oeiResRef, ICON_RESOURCE_TYPE);
                if (imageResource != null)
                    {
                    itemIcon = SpecialBitmapLoader.LoadImageFromStream(imageResource.GetStream(false));
                    if (itemIcon == null)
                        {
                        itemIcon = System.Drawing.SystemIcons.Question.ToBitmap();
                        }
                    else
                        {
                        Bitmap tmpImg = resizeImage(itemIcon);
                        itemIcon.Dispose();
                        itemIcon = tmpImg;
                        }
                    }
                }
            grid.Rows[index].Cells["propsQuestImage"].Value = itemIcon;
            grid.Rows[index].Cells["propsQuestImage"].ToolTipText = imageFilename;
            grid.AutoResizeRow(index, DataGridViewAutoSizeRowMode.AllCellsExceptHeader);
            }

        /// <summary>
        /// Method needed to ensure that I can load and save assemblies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
            {
            Assembly ayResult = null;
            string sShortAssemblyName = null;
            if (args != null)
                {
                sShortAssemblyName = args.Name.Split(',')[0];
                }

            Assembly[] ayAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly ayAssembly in ayAssemblies)
                {
                if (sShortAssemblyName == ayAssembly.FullName.Split(',')[0])
                    {
                    ayResult = ayAssembly;
                    break;
                    }
                }
            return ayResult;
            }

        /// <summary>
        /// Resets the getBlueprint form, so that when I load it in the future, its list of actors will be reloaded
        /// </summary>
        private void QuestMain_FormClosing(object sender, FormClosingEventArgs e)
            {
            // i reset the blueprint from - so that the list of actors can be reloaded
            getBlueprint.resetBlueprintForm();
            }

        /// <summary>
        /// 
        /// </summary>
        private void propDelete_Click(object sender, EventArgs e)
            {
            remove(PropGrid, propEdit, propRemove, ref props, "propData");
            }

        /// <summary>
        /// Removes the selected actors
        /// </summary>
        /// <param name="grid">The grid where the actor will be inserted</param>
        /// <param name="editButton">The edit button that will be enabled (if there is an actor)</param>
        /// <param name="removeButton">Ditto for the removeButton</param>
        /// <param name="type">The type of the new blueprint</param>
        /// <param name="list">The list that the new actor will be added to</param>
        /// <param name="dataCell">The name of the data cell in the grid</param>
        /// <returns>The updated list with the actors</returns>
        private static LinkedList<Actor> remove(DataGridView grid, Button editButton, Button removeButton, ref LinkedList<Actor> list, String dataCell)
            {
            if (grid.SelectedRows.Count > 0)
                {
                foreach (DataGridViewRow row in grid.SelectedRows)
                    {
                    var cell = row.Cells[dataCell];
                    var actor = (Actor)cell.Value;
                    list.Remove(actor);
                    grid.Rows.Remove(row);
                    }
                }
            if (1 > grid.RowCount)
                {
                editButton.Enabled = false;
                removeButton.Enabled = false;
                }
            return list;
            }

        /// <summary>
        /// Remove button for triggers
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
            {
            remove(triggerRegionsGrid, triggerEdit, triggerRemove, ref triggers, "triggerData");
            }

        /// <summary>
        /// Tries to find a path to save and load from
        /// </summary>
        /// <returns>Whether it found a path to save and load from</returns>
        private Boolean getPath()
            {
            Boolean foundDirectory = false;

            var browser = new System.Windows.Forms.FolderBrowserDialog();
            browser.Description = "Please select the folder you wish to save and load your quest templates to";
            if (browser.ShowDialog() == DialogResult.OK)
                {
                filePath = browser.SelectedPath;
                }

            if (filePath != null)
                foundDirectory = true;

            return foundDirectory;
            }

        /// <summary>
        /// Updates the selected gender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void genderBox_SelectedIndexChanged(object sender, EventArgs e)
            {
            gender = (BWLanguages.Gender)genderBox.SelectedIndex;
            setLang();
            }

        #region Utility Methods
        /// <summary>
        /// Scales a bitmap to correct proportions
        /// </summary>
        /// <param name="inputBmp">The bitmap to be scaled</param>
        /// <returns>Returns a scaled bitmap</returns>
        private static Bitmap resizeImage(Bitmap inputBmp)
            {
            Rectangle cropArea = new Rectangle(0, 0, 40, 40);
            Bitmap newBmp = new Bitmap(40, 40, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            //Graphics.FromImage doesn't like Indexed pixel format

            //Create a graphics object and attach it to the bitmap
            Graphics newBmpGraphics = Graphics.FromImage(newBmp);

            //Draw the portion of the input image in the crop rectangle
            //in the graphics object
            newBmpGraphics.DrawImage(inputBmp,
                cropArea,
                cropArea,
                GraphicsUnit.Pixel);

            //Return the bitmap
            newBmpGraphics.Dispose();

            return newBmp;
            }

        /// <summary>
        /// Writes a string to the console
        /// </summary>
        /// <param name="str">The string to write to the console</param>
        private static void debug(String str)
            {
            System.Console.WriteLine(str);
            }

        /// <summary>
        /// Creates a messagebox with the given string
        /// </summary>
        /// <param name="str">The string to print to the user</param>
        private static void report(String str)
            {
            System.Windows.Forms.MessageBox.Show(str);
            }

        /// <summary>
        /// Resets the grid - i.e. removes all entires
        /// </summary>
        /// <param name="grid">The grid to reset</param>
        private static void resetGrid(DataGridView grid)
            {
            if (grid.Rows != null && grid.Rows.Count > 0)
                {
                debug("Row count: " + grid.Rows.Count);
                grid.Rows.Clear();
                }

            }
        #endregion

        #region Edit
        /// <summary>
        /// Edit the giver
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void giverEdit_Click(object sender, EventArgs e)
            {
            giver = singleActor(GiverGrid, "giverData");
            }

        /// <summary>
        /// Edit a actor in a multiple actor grid
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="type">The type of actor</param>
        /// <param name="dataName">The name of the datacell</param>
        /// <param name="list">The list of actors (extra, item, triggers)</param>
        private static void editActor(DataGridView grid, EnumTypes.actorType type, String dataName, LinkedList<Actor> list)
            {
            if (grid.SelectedRows.Count > 0)
                {
                int gridNumber = grid.SelectedRows[0].Index;
                Actor oldActor = gridGetActor(grid, dataName);
                if (oldActor != null)
                    {
                    modifyForm = ModifyNew.getModifyNew(oldActor, type);
                    if (modifyForm.ShowDialog() == DialogResult.OK)
                        {
                        Actor actor = ModifyNew.lastActor;
                        grid[0, gridNumber].Value = actor.ToString();
                        grid[1, gridNumber].Value = actor.Tag;
                        grid[dataName, gridNumber].Value = actor;
                        if (list != null)
                            {
                            list.Remove(oldActor);
                            list.AddLast(actor);
                            }
                        }
                    }
                }
            }

        /// <summary>
        /// Edit the villian
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void villianEdit_Click(object sender, EventArgs e)
            {
            villian = singleActor(VillianGrid, "villianData");
            }

        /// <summary>
        /// Edits a single actor in a grid
        /// </summary>
        /// <param name="grid">The grid from where the actor to be edited will come from</param>
        /// <param name="dataName">The name of the dataCell in the grid</param>
        /// <returns>The actor that has been added</returns>
        private Actor singleActor(DataGridView grid, String dataName)
            {
            grid.Rows[0].Selected = true;
            var type = EnumTypes.actorType.Creature | EnumTypes.actorType.Door
                | EnumTypes.actorType.Placeable;
            editActor(grid, type, dataName, null);
            return (Actor)grid[dataName, 0].Value;
            }

        /// <summary>
        /// Edit an extra (creature, door or placeable)
        /// </summary>
        private void extraEdit_Click(object sender, EventArgs e)
            {
            var type = EnumTypes.actorType.Creature | EnumTypes.actorType.Door
                | EnumTypes.actorType.Placeable;
            editActor(ExtraGrid, type, "extraData", extra);
            }

        /// <summary>
        /// Edtirs an item actor
        /// </summary>
        private void propEdit_Click(object sender, EventArgs e)
            {
            editActor(PropGrid, EnumTypes.actorType.Item, "propData", props);
            }

        /// <summary>
        /// Edit a trigger actor
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
            {
            editActor(triggerRegionsGrid, EnumTypes.actorType.TriggerRegion, "triggerData", triggers);
            }

        /// <summary>
        /// Gets the actor from the grid
        /// </summary>
        /// <param name="grid">The grid from where the actor is extracted</param>
        /// <param name="dataName">The name cell in the grid that contains the data</param>
        /// <returns>The actor contained in the row</returns>
        private static Actor gridGetActor(DataGridView grid, String dataName)
            {
            return (Actor)grid[dataName, grid.SelectedRows[0].Index].Value;
            }
        #endregion

        #region New

        /// <summary>
        /// Create a new quest giver
        /// </summary>
        private void giverNew_Click(object sender, EventArgs e)
            {
            newClick(ref giver, GiverGrid, giverEdit, null, EnumTypes.actorType.Creature | EnumTypes.actorType.Door
                | EnumTypes.actorType.Placeable, "giverData");
            if (giver != null)
                newsNodeButton.Enabled = true;
            }

        /// <summary>
        /// Create a new villian
        /// </summary>
        private void villianNew_Click(object sender, EventArgs e)
            {
            newClick(ref villian, VillianGrid, villianEdit, villianRemove, EnumTypes.actorType.Creature | EnumTypes.actorType.Door
                | EnumTypes.actorType.Placeable, "villianData");
            }

        private void newClick(ref Actor primeActor, DataGridView grid, Button editButton, Button removeButton,
    EnumTypes.actorType type, String dataCell)
            {
            LinkedList<Actor> actors = new LinkedList<Actor>();
            insertActor(grid, type, ref actors, dataCell);
            if (actors.Count > 0)
                {
                primeActor = actors.First.Value;
                }
            }

        /// <summary>
        /// Attempts to create a new extra (which can be a create, door or placeable
        /// </summary>
        private void extraNew_Click(object sender, EventArgs e)
            {
            insertActor(ExtraGrid, EnumTypes.actorType.Creature | EnumTypes.actorType.Door
                | EnumTypes.actorType.Placeable, ref extra, "extraData");
            }

        /// <summary>
        /// Attempts to create a new item
        /// </summary>
        void propNew_Click(object sender, EventArgs e)
            {
            insertActor(PropGrid, EnumTypes.actorType.Item, ref props, "propData");
            }

        /// <summary>
        /// Attempts to create a new trigger
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
            {
            insertActor(triggerRegionsGrid, EnumTypes.actorType.TriggerRegion, ref triggers, "triggerData");
            }

        /// <summary>
        /// Inserts an actor in the grid
        /// </summary>
        /// <param name="grid">The grid where the actor will be inserted</param>
        /// <param name="editButton">The edit button that will be enabled (if there is an actor)</param>
        /// <param name="removeButton">Ditto for the removeButton</param>
        /// <param name="type">The type of the new blueprint</param>
        /// <param name="list">The list that the new actor will be added to</param>
        /// <param name="dataName">The name of the data cell in the grid</param>
        private static void insertActor(DataGridView grid,
    EnumTypes.actorType type, ref LinkedList<Actor> list, String dataCell)
            {
            modifyForm = ModifyNew.getModifyNew(type);

            // If the user wants to create a new blueprint, and there is only one type, then we might as well create that
            modifyForm.testCreate();

            Actor actor = null;
            if (modifyForm.ShowDialog() == DialogResult.OK)
                {
                actor = ModifyNew.lastActor;
                if (actor != null)
                    {
                    LinkedList<Actor> dummyList = new LinkedList<Actor>();
                    dummyList.AddLast(actor);
                    setGrid(grid, dummyList, list, dataCell, type);
                    }
                }
            }
        #endregion

        private void editRemoveStatus(object sender, EventArgs e)
            {
            debug("Selection changed");
            DataGridView view  = (DataGridView)sender;
            bool viewContent = view.RowCount > 0;
            debug("Name " + view.Name);
            if (view == VillianGrid)
                {
                villianEdit.Enabled = viewContent;
                villianRemove.Enabled = viewContent;
                }
            else if (view == GiverGrid)
                {
                giverEdit.Enabled = viewContent;
                }
            else if (view == ExtraGrid)
                {
                extraEdit.Enabled = viewContent;
                extraRemove.Enabled = viewContent;
                }
            else if (view == PropGrid)
                {
                propEdit.Enabled = viewContent;
                propRemove.Enabled = viewContent;
                }
            else if (view == triggerRegionsGrid)
                {
                triggerEdit.Enabled = viewContent;
                triggerRemove.Enabled = viewContent;
                }
            }

        }
    }
