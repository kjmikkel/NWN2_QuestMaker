/*
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using NWN2Toolset;
using NWN2Toolset.NWN2.Data;
using NWN2Toolset.NWN2.Data.Journal;
using NWN2Toolset.NWN2.Data.Blueprints;
using NWN2Toolset.NWN2.Data.Templates;
using NWN2Toolset.NWN2.Data.ConversationData;
using NWN2Toolset.NWN2.Data.Instances;
using NWN2Toolset.NWN2.Data.Factions;

using NWN2Toolset.NWN2.Dialogs;
using NWN2Toolset.NWN2.Wizards;

using OEIShared.Utils;
using OEIShared.IO;
using NWN2Toolset.NWN2.Data.Campaign;

namespace QuestMaker
    {
    [Serializable]
    public class StoryNode
        {
        #region Fields
        public Actor actor;
        public Actor trigger;

        [NonSerialized]
        private static NWN2GameModule module;

        private string name;

        public enum convType : byte
            {
            None, Escort, Explore
            }

        public convType convHappens;
        public convType triggerHappens;
        public EnumTypes.happens happensEnum;
        public EnumTypes.conv convEnum;
        public EnumTypes.prereq preReq;

        public StoryNode preReqNode;

        // The quest identifier for this node
        public int questId;

        public bool containerContains;
        public bool villianTalk;
        public bool villianGotItem;
        public bool endPoint;
        public bool journalCheck = true;

        public Actor takeItem;
        public Actor giveItem;
        public Actor villianItem;

        public int takeNumber = 1;
        public int giveNumber = 1;

        public int takeGold;
        public int giveGold;

        // Conversation
        public string greeting;
        public string acceptence;
        public string action;
        public string rejection;

        private static Hashtable alreadyWiped = new Hashtable();

        public string journal; // The content of the journal entry
        public int xp;         // The amount of xp given for completing the part of the jouranl
        #endregion

        /// <summary>
        /// Constructor for the story node
        /// </summary>
        /// <param name="name">The name of the story node</param>
        /// <param name="actor">The actor who will be principal in the story node</param>
        /// <param name="trigger">The trigger, if any, that will be associated with the story node</param>
        /// <param name="inModule">The module that is connected to the story node</param>
        public StoryNode(String name, Actor actor, Actor trigger, NWN2GameModule inModule)
            {
            this.name = name;
            this.actor = actor;
            this.trigger = trigger;
            module = inModule;
            }

        public static void wipeConvHash()
            {
            alreadyWiped.Clear();
            }

        /// <summary>
        /// Either creates or add an entry to the journal that goes with the quest - there will only be one journal pr. quest
        /// </summary>
        /// <param name="name">The name of the journal category</param>
        /// <param name="priority">The priority of the journal entry</param>
        /// <param name="gender">The gender of the journal entry</param>
        /// <param name="lang">The language of the journal</param>
        /// <param name="category">The category that belong to the quest</param>
        /// <returns>The created journal</returns>
        public NWN2JournalCategory createJournal(string name, NWN2JournalPriority priority, BWLanguages.Gender gender, BWLanguages.BWLanguage lang, NWN2JournalCategory category)
            {
            int localQuestID = (questId == 0) ? 1 : questId;
            string tag = "Q_" + questNamePrep(name);
            foreach (NWN2JournalCategory jourCat in module.Journal.Categories)
                {
                if (jourCat.Tag == tag)
                    {
                    category = jourCat;
                    break;
                    }
                }

            if (category == null)
                {
                category = module.Journal.AddCategory();
                category.Name.SetString(name, lang, gender);

                category.Priority = priority;

                //I create the tag
                category.Tag = tag;
                }
            LinkedList<NWN2JournalEntry> journalEnties = new LinkedList<NWN2JournalEntry>();
            NWN2JournalEntry tempJournalEntry;

            if (localQuestID == 1)
                {
                /*
                for (int i = 0; i < category.Entries.Count; i++)
                    {
                    tempJournalEntry = category.Entries[i];
                    if ("pluginGenerated:" + localQuestID.ToString() == tempJournalEntry.Comment)
                        {
                        debug("equal comment");
                        journalEnties.AddLast(tempJournalEntry);
                        break;
                        }
                    }
                
                foreach (NWN2JournalEntry tempJournal in journalEnties)
                    {
                    debug("journal entry: " + tempJournal.Text[lang]);
                    category.Entries.Remove(tempJournal);
                    }
                 * */
                // If we are starting at a new quest, and there are risidual objects, we just clear them all
                category.Entries.Clear();
                }
            NWN2JournalEntry entry = category.AddEntry();
            entry.ID = (uint)localQuestID;
            entry.Endpoint = endPoint;
            entry.Comment = "pluginGenerated:" + entry.ID;
            entry.Text.SetString(journal, lang, gender);
            return category;
            }

        #region Conversation related

        /// <summary>
        /// Adds a conversation to an actor
        /// </summary>
        /// <param name="questName">The name of the quset</param>
        /// <param name="gender">The gender which will be used in the quest</param>
        /// <param name="lang">The spoken (real world) language</param>
        public void addConv(string questName, BWLanguages.Gender gender, BWLanguages.BWLanguage lang)
            {
            questName = questNamePrep(questName);
            NWN2GameConversation cConversation = null;
            if (happensEnum == EnumTypes.happens.Conv)
                {
                cConversation = conversationFixer(questName, gender, lang);
                }
            else if (happensEnum == EnumTypes.happens.Conflict)
                {
                cConversation = conflictFixer(questName, gender, lang);
                }
            else if (happensEnum == EnumTypes.happens.OpenDoor)
                {
                doorFixer(questName, gender, lang);
                }
            else
                {
                cConversation = triggerFixer(questName, gender, lang);
                }

            if (cConversation != null)
                module.AddResource(cConversation);
            }

        /// <summary>
        /// If there already exists a conversation by the name in the module, then it is fetched, otherwise a new is created
        /// </summary>
        /// <param name="questName">The name of the quset</param>
        /// <param name="gender">The gender which will be used in the quest</param>
        /// <param name="lang">The spoken (real world) language</param>
        /// <returns>The conversation</returns>
        private NWN2GameConversation conversationFixer(string questName, BWLanguages.Gender gender, BWLanguages.BWLanguage lang)
            {
            String convName = String.Empty;
            NWN2GameConversation cConversation = null;

            /* If the actor is a creature and does not yet belong to a faction, 
             * I set it to be a commoner.
             */
            #region factionSet
            if (actor.type == EnumTypes.actorType.Creature)
                {
                if (actor.boolInstance)
                    {
                    NWN2CreatureInstance inst = (NWN2CreatureInstance)actor.instance;
                    if (inst.FactionID < 0 || inst.FactionID > 12)
                        {
                        inst.FactionID = 2;
                        }
                    }
                else
                    {
                    NWN2CreatureBlueprint blue = (NWN2CreatureBlueprint)actor.blueprint;
                    if (blue.FactionID < 0 || blue.FactionID > 12)
                        {
                        blue.FactionID = 2;
                        }
                    }
                }
            #endregion

            #region getConversationName
            if (actor.Conversation != null && System.IO.Path.GetFileNameWithoutExtension(actor.Conversation.FullName) != String.Empty)
                {
                // I get the name of what the conversation is/should be called
                convName = System.IO.Path.GetFileNameWithoutExtension(actor.Conversation.FullName);
                }
            else if (trigger != null)
                {
                convName = trigger.ToString() + "Conv";
                }
            else
                {
                convName = questNamePrep(questName) + "_" + actor.ToString().Replace(" ", "");
                }
            cConversation = module.Conversations[convName];
            #endregion

            #region ensureConversation
            // If we still have no conversation, then we create one 
            if (cConversation == null)
                {
                cConversation = new NWN2GameConversation(convName, module.Repository.DirectoryName, module.Repository);
                cConversation.Demand();
                }
            else
                {
                cConversation.Demand();
                // if we have a conversation, and we have not cleaned it yet - then we do so now, and makes
                // sure that no one else cleans it
                if (alreadyWiped[cConversation.Name] == null)
                    {
                    alreadyWiped[cConversation.Name] = true;

                    NWN2ConversationConnectorCollection connectors = cConversation.StartingList;
                    LinkedList<NWN2ConversationConnector> removeList = new LinkedList<NWN2ConversationConnector>();

                    foreach (NWN2ConversationConnector connnect in connectors)
                        {
                        if (connnect.Comment.Contains("pluginGenerated: " + questName + ":" + questId.ToString()))
                            {
                            removeList.AddLast(connnect);
                            }
                        }

                    debug("Number of convs to remove: " + removeList.Count);
                    foreach (NWN2ConversationConnector removeConnect in removeList)
                        {
                        debug("Removed conversation: " + removeConnect.Text[lang]);
                        cConversation.RemoveNode(removeConnect);
                        }
                    }
                }
            cConversation.OEISerialize(true);
            cConversation.NWN1StyleDialogue = true;
            #endregion
            cConversation = constructConv(cConversation, questName, gender, lang);
            #region FixDoorTalk
            if (actor.type == EnumTypes.actorType.Placeable || actor.type == EnumTypes.actorType.Door)
                {
                NWN2GameScript talkScript = module.Scripts["ga_door_talk"];
                if (talkScript == null)
                    {
                    talkScript = new NWN2GameScript("ga_door_talk", module.Repository.DirectoryName, module.Repository);
                    talkScript.Data = Properties.Resources.ga_door_talk;
                    talkScript.OEISerialize();
                    module.Scripts.Add(talkScript);
                    }
                actor.doorTalk = talkScript.Resource;
                }
            #endregion
            if (triggerHappens != convType.Explore)
                actor.Conversation = cConversation.Resource;
            return cConversation;
            }

        /// <summary>
        /// Creates a conversation that contains a conflict (i.e. the actor will turn hostile)
        /// </summary>
        /// <param name="questName">The name of the quset</param>
        /// <param name="gender">The gender which will be used in the quest</param>
        /// <param name="lang">The spoken (real world) language</param>
        /// <returns>The conversation</returns>
        private NWN2GameConversation conflictFixer(string questName, BWLanguages.Gender gender, BWLanguages.BWLanguage lang)
            {
            // I set the villian to drop the needed loot
            addInv();

            NWN2GameConversation cConversation = null;

            /* If the villian does not talk to the player, but just attacks, 
            then we don't need to any of the following */

            if (!villianTalk)
                {
                ushort hostile = 0;
                foreach (NWN2Faction fact in module.FactionData.Factions)
                    {
                    if (fact.Name == "Hostile")
                        {
                        hostile = (ushort)fact.ID;
                        debug("Value for hostile: " + hostile.ToString());
                        break;
                        }
                    }
                if (actor.boolInstance)
                    {
                    NWN2CreatureInstance insActor = (NWN2CreatureInstance)actor.instance;
                    insActor.FactionID = hostile;
                    }
                else
                    {
                    NWN2CreatureBlueprint bluActor = (NWN2CreatureBlueprint)actor.blueprint;
                    bluActor.FactionID = hostile;
                    }
                }
            else
                {
                NWN2GameScript killActor = getAdvanceJournal();

                questName = questNamePrep(questName);
                NWN2ScriptVarTable varTable;
                NWN2CreatureBlueprint creatureBlue = null;
                NWN2CreatureInstance creatureInst = null;

                if (actor.boolInstance)
                    {
                    creatureInst = (NWN2CreatureInstance)actor.instance;
                    varTable = creatureInst.Variables;
                    }
                else
                    {
                    creatureBlue = (NWN2CreatureBlueprint)actor.blueprint;
                    varTable = creatureBlue.Variables;
                    }
                if (varTable == null)
                    {
                    varTable = new NWN2ScriptVarTable();
                    }

                // I remove any old duplicates
                LinkedList<string> toRemove = new LinkedList<string>();
                toRemove.AddLast("DeathScript");
                varTable = getAdvanceJournalVar(varTable, questName, questId, xp, toRemove);
                var deathScript = new NWN2ScriptVariable("DeathScript", "ga_advance_journal");
                varTable.Add(deathScript);

                if (creatureInst != null)
                    {
                    creatureInst.Variables = varTable;
                    }
                else if (creatureBlue != null)
                    {
                    creatureBlue.Variables = varTable;
                    }

                cConversation = conversationFixer(questName, gender, lang);
                }
            return cConversation;
            }

        private static string questNamePrep(string questName)
            {
            return questName.Replace(" ", "").Trim();
            }

        /// <summary>
        /// Currently not correctly implemented - this should make sure that once you open a door/gets an item then the quest is updated
        /// I have yet to find a way to do this without writing a new script - but perhaps you have an idea - if so you are more than welcome 
        /// to contact me
        /// </summary>
        /// <param name="questName">The name of the quset</param>
        /// <param name="gender">The gender which will be used in the quest</param>
        /// <param name="lang">The spoken (real world) language</param>
        /// <returns>The conversation</returns>
        private void doorFixer(string questName, BWLanguages.Gender gender, BWLanguages.BWLanguage lang)
            {
            addInv();
            NWN2GameScript advanceScript = getAdvanceJournal();

            var varTable = new NWN2ScriptVarTable();
            varTable = getAdvanceJournalVar(varTable, questName, questId, xp);
            var resource = advanceScript.Resource;
            if (actor.type == EnumTypes.actorType.Door)
                {
                if (actor.boolInstance)
                    {
                    var instDoor = (NWN2DoorInstance)actor.instance;
                    instDoor.Variables = varTable;
                    instDoor.OnDeath = advanceScript.Resource;
                    instDoor.OnOpen = resource;
                    }
                else
                    {
                    var blueDoor = (NWN2DoorBlueprint)actor.blueprint;
                    blueDoor.Variables = varTable;
                    blueDoor.OnDeath = resource;
                    blueDoor.OnOpen = resource;
                    }
                }
            else if (actor.type == EnumTypes.actorType.Placeable)
                {
                if (actor.boolInstance)
                    {
                    var instPlaceable = (NWN2PlaceableInstance)actor.instance;
                    instPlaceable.Variables = varTable;
                    instPlaceable.OnDeath = resource;
                    if (containerContains && villianItem != null)
                        {
                        NWN2InstanceInventoryItem instanceItem = null;
                        if (villianItem.boolInstance)
                            {
                            instanceItem = createInstanceItemInfo((NWN2ItemInstance)villianItem.instance, false, false);
                            }
                        else
                            {
                            instanceItem = createInstanceItemInfo((NWN2ItemBlueprint)villianItem.blueprint, false, false);
                            }
                        instPlaceable.Inventory.Add(instanceItem);
                        instPlaceable.OnInvDisturbed = resource;
                        }
                    else
                        {
                        instPlaceable.OnOpen = resource;
                        }
                    }
                else
                    {
                    var bluePlaceable = (NWN2PlaceableBlueprint)actor.blueprint;
                    bluePlaceable.Variables = varTable;
                    bluePlaceable.OnDeath = resource;
                    if (containerContains && villianItem != null)
                        {
                        NWN2BlueprintInventoryItem blueItemItem = null;
                        if (villianItem.boolInstance)
                            {
                            blueItemItem = createBlueprintItemInfo((NWN2ItemInstance)villianItem.instance, false, false);
                            }
                        else
                            {
                            blueItemItem = createBlueprintItemInfo((NWN2ItemBlueprint)villianItem.blueprint, false, false);
                            }
                        bluePlaceable.Inventory.Add(blueItemItem);
                        bluePlaceable.OnInvDisturbed = resource;
                        }
                    else
                        {
                        bluePlaceable.OnOpen = resource;
                        }
                    }
                }
            }

        private static NWN2ScriptVarTable removeDuplicates(NWN2ScriptVarTable varTable, LinkedList<String> toRemove)
            {
            // I remove any old duplicates
            LinkedList<NWN2ScriptVariable> varList = new LinkedList<NWN2ScriptVariable>();
            foreach (NWN2ScriptVariable var in varTable.ToArray())
                {
                if (toRemove.Contains(var.Name))
                    {
                    varList.AddLast(var);
                    }
                }
            foreach (NWN2ScriptVariable var in varList)
                {
                varTable.Remove(var);
                }
            return varTable;
            }

        private static NWN2ScriptVarTable getAdvanceJournalVar(NWN2ScriptVarTable varTable, string questName, int questId, int xp)
            {
            LinkedList<string> toRemove = new LinkedList<string>();
            return getAdvanceJournalVar(varTable, questName, questId, xp, toRemove);
            }

        private static NWN2ScriptVarTable getAdvanceJournalVar(NWN2ScriptVarTable varTable, string questName, int questId, int xp, LinkedList<string> toRemove)
            {
            toRemove.AddLast("XP");
            toRemove.AddLast("JournalUpdate");
            toRemove.AddLast("nextState");
            removeDuplicates(varTable, toRemove);
            var XP = new NWN2ScriptVariable("XP", xp);
            var journalCategory = new NWN2ScriptVariable("JournalUpdate", "Q_" + questName);
            int state = (questId == 0) ? 1 : questId;
            var journalEntry = new NWN2ScriptVariable("nextState", state);
            varTable.Add(XP);
            varTable.Add(journalCategory);
            varTable.Add(journalEntry);
            return varTable;
            }

        /// <summary>
        /// Sets all the triggers values so that it has the correct triggers
        /// </summary>
        /// <param name="questName">The name of the quset</param>
        /// <param name="gender">The gender which will be used in the quest</param>
        /// <param name="lang">The spoken (real world) language</param>
        /// <returns>The conversation</returns>
        private NWN2GameConversation triggerFixer(string questName, BWLanguages.Gender gender, BWLanguages.BWLanguage lang)
            {
            NWN2GameConversation cConversation = null;
            if (triggerHappens == convType.Explore)
                {
                NWN2ScriptVarTable varTable = trigger.Var;
                if (varTable == null)
                    {
                    varTable = new NWN2ScriptVarTable();
                    }
                condAdvance(trigger, varTable, questName, questId, xp, "ga_advance_journal", "XP granted for exploration");
                }
            else if (triggerHappens == convType.Escort)
                {
                cConversation = conversationFixer(questName, gender, lang);
                makeEscort(actor.Tag, trigger, cConversation.Name, questName, questId);
                }
            return cConversation;
            }

        private void condAdvance(Actor trigger, NWN2ScriptVarTable varTable, string questName, int questId, int xp, string script, string message)
            {
            LinkedList<String> toRemove = new LinkedList<string>();
            toRemove.AddLast("negate");
            toRemove.AddLast("condition");
            toRemove.AddLast("script");
            toRemove.AddLast("message");
            varTable = getAdvanceJournalVar(varTable, questName, questId, xp, toRemove);
            string value = "";
            if (preReq == EnumTypes.prereq.SimplePrereq || preReq == EnumTypes.prereq.CastSpecificPrereq)
                {
                int questValue = preReqNode.questId;
                if (questValue == 0) questValue = 1;
                value = questValue.ToString();
                }

            NWN2GameScript sayString = module.Scripts["gtr_conditional_run_script"];
            if (sayString == null)
                {
                sayString = new NWN2GameScript("gtr_conditional_run_script", module.Repository.DirectoryName, module.Repository);
                sayString.Data = Properties.Resources.gtr_conditional_run_script;
                sayString.OEISerialize();
                module.Scripts.Add(sayString);
                }

           

            if (preReq == EnumTypes.prereq.SimplePrereq)
                {
                value = "<" + value;
                NWN2ScriptVariable negate = new NWN2ScriptVariable("negate", 1);
                varTable.Add(negate);
                }

            NWN2ScriptVariable compare1 = new NWN2ScriptVariable("condition", value);
            NWN2ScriptVariable scriptVar = new NWN2ScriptVariable("script", script);
            if (message != "")
                {
                NWN2ScriptVariable messageVar = new NWN2ScriptVariable("message", message);
                varTable.Add(messageVar);
                }

            varTable.Add(compare1);
            varTable.Add(scriptVar);
            
            trigger.Var = varTable;
            if (trigger.boolInstance)
                {
                ((NWN2TriggerInstance)trigger.instance).OnEnter = sayString.Resource;
                }
            else
                {
                ((NWN2TriggerBlueprint)trigger.blueprint).OnEnter = sayString.Resource;
                }
            }

        /// <summary>
        /// The method that makes sure the correct scripts and conditionas are set on the conversation
        /// </summary>
        /// <param name="cConversation">The conversation</param>
        /// <param name="questName">The name of the quset</param>
        /// <param name="gender">The gender which will be used in the quest</param>
        /// <param name="lang">The spoken (real world) language</param>
        /// <returns>The conversation</returns>
        private NWN2GameConversation constructConv(NWN2GameConversation cConversation, String questName, BWLanguages.Gender gender, BWLanguages.BWLanguage lang)
            {
            // Done in order to avoid a side condition
            int value = (questId == 0) ? 1 : questId;
            /*
            if (happensEnum == EnumTypes.happens.Trigger && triggerHappens == convType.Explore)
                {
                var tempConv = cConversation.InsertChild(null);
                tempConv.Text.SetString(String.Empty, lang, gender);
                tempConv.Speaker = trigger.Tag;
                tempConv.Comment = "pluginGenerated:" + value;
                tempConv.ShowOnce = NWN2ConversationShowOnceType.OncePerModule;
//                cConversation.InsertChild(tempConv);
                return cConversation;
                }
            */
            // I add it to null to indicate that this will be at the root
            NWN2ConversationConnector conv = cConversation.InsertChild(null);
            // I set the comments so that it later can be deleted (if you choose to recreate the quest)
            conv.Comment = "pluginGenerated: " + questName + ":" + questId.ToString();

            // I begin the conversation
            debug("Begin conversation");
            conv.Text.SetString(greeting, lang, gender);
            // I make sure it is only fired once if we are talking escort/exploration

            NWN2ConversationConnector accept;
            NWN2ConversationConnector quest;
            NWN2ConversationConnector reject;

            #region Conditions
            // if this requires a node to fire, then we add the requirement here
            if (// If there is a prerequisite
                preReq != EnumTypes.prereq.NoPrereq
                || // or if we are exploring or escorting 
                happensEnum == EnumTypes.happens.Trigger
                //&& (convHappens == convType.Escort || convHappens == convType.Explore)
                )
                {
                debug("conditional");
                conv.Conditions.Add(conditional(questName));
                if (happensEnum == EnumTypes.happens.Trigger || convHappens != convType.None)
                    {
                    // It must be inserted as the first thing
                    conv.Conditions.Insert(0, node());
                    }
                }

            // Once the offer has been accepted it should not appear again
            debug("makeJournal");
            conv.Conditions.Add(makeJournalCheck(questName, "<" + value));
            #endregion

            // If this is only a single comment I add the journal entry - if there is one (or we are opening a door)
            if (happensEnum == EnumTypes.happens.Trigger || happensEnum == EnumTypes.happens.OpenDoor ||
                (convEnum == EnumTypes.conv.Single &&
                /* since I have made sure that the villian only can call this if he is actually talking, 
                 we only need to make sure that we are doing something
                 */
                happensEnum != EnumTypes.happens.None))
                {
                debug("setConvValues");
                setConvValues(ref conv, ref conv, questName);
                /* If we are not dealing with a villain, then this
                 is needed to make quest updates fire otherwise any quest update will not fire */
                if (happensEnum != EnumTypes.happens.Conflict)
                    cConversation.InsertChild(conv);
                }
            else
                #region Quest
                if (convEnum == EnumTypes.conv.QuestInfo && happensEnum != EnumTypes.happens.Trigger && happensEnum != EnumTypes.happens.None)
                    {
                    // The player accepts
                    accept = cConversation.InsertChild(conv);
                    accept.Text.SetString(acceptence, lang, gender);

                    // The quest giver responce
                    quest = cConversation.InsertChild(accept);
                    quest.Text.SetString(action, lang, gender);

                    setConvValues(ref accept, ref quest, questName);

                    // The player declines
                    reject = cConversation.InsertChild(conv);
                    reject.Text.SetString(rejection, lang, gender);
                    }
                #endregion
            return cConversation;
            }

        /// <summary>
        /// This adds many of the conditions and scripts to the conversation
        /// </summary>
        /// <param name="conv1">The first part of the conversation - this can be equal to the second</param>
        /// <param name="conv2">The second part of the conversation - this can be equal to the first</param>
        /// <param name="questName">The name of the quest</param>
        private void setConvValues(ref NWN2ConversationConnector conv1, ref NWN2ConversationConnector conv2, String questName)
            {
            if (takeGold > 0)
                {
                conv1.Conditions.Add(checkGold(giveGold));
                conv2.Actions.Add(giveOrTakeGold(false, giveGold));
                }

            if (takeItem != null)
                {
                conv1.Conditions.Add(checkItem(takeItem.Tag));
                conv2.Actions.Add(giveOrTakeItem(false, takeItem.Tag, takeNumber));
                }

            if (giveGold > 0)
                {
                conv2.Actions.Add(giveOrTakeGold(true, giveGold));
                }

            if (giveItem != null)
                {
                conv2.Actions.Add(giveOrTakeItem(true, giveItem.Tag, giveNumber));
                }

            // I add the advancement
            // conv1.Actions.Add(  //advanceQuest(questName, questId));

            if (convHappens == convType.Escort)
                {
                conv2.Actions.Add(addHenchMan(actor));
                }

            // Unless there we are dealing with a villain - we advance the quest
            if (happensEnum != EnumTypes.happens.Conflict)
                {
                conv2.Quest = makeQuest(questName, questId);
                //         conv2.Actions.Add(makeJournal(questName, questId));
                }
            /*
            if (happensEnum == EnumTypes.happens.Conv && convHappens != convType.None)
                {
                conv1.Actions.Add(EscortExplore(triggerHappens, convHappens, actor, trigger, name));
                }
            */
            if (xp > 0 && happensEnum != EnumTypes.happens.Conflict)
                {
                conv2.Actions.Add(XP(xp));
                }

            if (happensEnum == EnumTypes.happens.Trigger && triggerHappens == convType.Escort)
                {
                conv2.Actions.Add(removeHenchMan(actor));
                }

            //If we have a villian, he attacks
            if (happensEnum == EnumTypes.happens.Conflict)
                {
                conv2.Actions.Add(conflict());
                }
            }

        private static NWN2QuestTuple makeQuest(string questName, int questId)
            {
            NWN2QuestTuple tuple = new NWN2QuestTuple();
            tuple.Quest = "Q_" + questNamePrep(questName);
            int value = (questId == 0) ? 1 : questId;
            tuple.Entry = (uint)value;
            return tuple;
            }
        #endregion

        #region Inventory
        /// <summary>
        /// Adds the items to the actor as specified in the story node
        /// </summary>
        private void addInv()
            {
            if (villianGotItem)
                {
                if (actor.boolInstance)
                    {
                    NWN2InstanceInventoryItem instanceItem = createInstanceItemInfo((NWN2ItemInstance)villianItem.instance, true, true);
                    actor.addInv(instanceItem);
                    }
                else
                    {
                    NWN2BlueprintInventoryItem blueprintItem = createBlueprintItemInfo((NWN2ItemBlueprint)villianItem.blueprint, true, true);
                    actor.addInv(blueprintItem);
                    }
                }
            }

        // The following 2 methods comes from LazjensCPSInventoryManager 
        /// <summary>
        /// Creates a instance inventroy item
        /// </summary>
        /// <param name="item">The instance item we want to make into an instance inventory item</param>
        /// <param name="droppable">Whether the item should be dropable</param>
        /// <param name="pp">Whether the item should be pickpocketable</param>
        /// <returns>The instance inventory item</returns>
        private static NWN2InstanceInventoryItem createInstanceItemInfo(NWN2ItemInstance item, bool droppable, bool pp)
            {
            NWN2InstanceInventoryItem itemInfo = new NWN2InstanceInventoryItem();
            itemInfo.Item = item;

            itemInfo.Droppable = droppable;
            itemInfo.Pickpocketable = pp;
            itemInfo.InInventory = true;
            return itemInfo;
            }

        /// <summary>
        /// Create an instance inventory item from a blueprint
        /// </summary>
        /// <param name="blueprint">The blueprint to create the instance inventory item from</param>
        /// <param name="droppable">Whether the item should be dropable</param>
        /// <param name="pp">Whether the item should be pickpocketable</param>
        /// <returns>The instance inventory item</returns>
        private static NWN2InstanceInventoryItem createInstanceItemInfo(NWN2ItemBlueprint blueprint, bool droppable, bool pp)
            {
            NWN2InstanceInventoryItem itemInfo = new NWN2InstanceInventoryItem();
            itemInfo.Item = NWN2ItemInstance.CreateFromBlueprint(blueprint);

            itemInfo.Droppable = droppable;
            itemInfo.Pickpocketable = pp;
            itemInfo.InInventory = true;
            return itemInfo;
            }

        /// <summary>
        /// Create a blueprint inventory item from a blueprint
        /// </summary>
        /// <param name="item">The blueprint we want to turn into a Blueprint inventory Item</param>
        /// <param name="droppable">Whether the item should be dropable</param>
        /// <param name="pp">Whether the item should be pickpocketable</param>
        /// <returns>The blueprint inventory item</returns>
        private static NWN2BlueprintInventoryItem createBlueprintItemInfo(NWN2ItemBlueprint item, bool droppable, bool pp)
            {
            NWN2BlueprintInventoryItem itemInfo = new NWN2BlueprintInventoryItem();
            itemInfo.Item = item.Resource;
            itemInfo.Droppable = droppable;
            itemInfo.Pickpocketable = pp;
            itemInfo.InInventory = true;
            return itemInfo;
            }

        /// <summary>
        /// Turns an Item instance into a blueprint inventory item
        /// </summary>
        /// <param name="item">The iteminstance we want to turn into a Blueprint inventory Item</param>
        /// <param name="droppable">Whether the item should be dropable</param>
        /// <param name="pp">Whether the item should be pickpocketable</param>
        /// <returns>The blueprint inventory item</returns>
        private NWN2BlueprintInventoryItem createBlueprintItemInfo(NWN2ItemInstance item, bool droppable, bool pp)
            {
            var blueItem = NWN2ItemInstance.CreateBlueprintFromInstance(item, module.Repository, false);

            NWN2BlueprintInventoryItem itemInfo = new NWN2BlueprintInventoryItem();
            itemInfo.Item = blueItem.Resource;
            itemInfo.Droppable = droppable;
            itemInfo.Pickpocketable = pp;
            itemInfo.InInventory = true;
            return itemInfo;
            }
        #endregion

        #region static NWN2ScriptFunctors

        /// <summary>
        /// Sets a global integer related to the quest
        /// </summary>
        /// <param name="questName">The name of the quest</param>
        /// <param name="questId">The id of the quset</param>
        /// <returns>The script funktor</returns>
        private static NWN2ScriptFunctor intSet(String questName, int questId)
            {
            NWN2ScriptFunctor setGlobalInt = new NWN2ScriptFunctor();
            setGlobalInt.Script = makeScript("ga_global_int");
            int setValue = questId;
            if (setValue == 0) setValue = 1;

            setParam(ref setGlobalInt, 0, questName);
            // The value here must be a string - so that values such as "+3" can give any sense
            setParam(ref setGlobalInt, 1, setValue.ToString());
            return setGlobalInt;
            }

        /// <summary>
        /// Makes sure the journal entry is updated
        /// </summary>
        /// <param name="questName">The name of the quest</param>
        /// <param name="questId">The id of the quset</param>
        /// <returns>The script funktor</returns>
        private static NWN2ScriptFunctor makeJournal(string questName, int questId)
            {
            // I create the functer
            NWN2ScriptFunctor journalFunctor = new NWN2ScriptFunctor();
            journalFunctor.Script = makeScript("ga_journal");
            //    char[] spiltArray = { ' ' };
            questName = "Q_" + questNamePrep(questName);

            // I begin to add the parameters
            setParam(ref journalFunctor, 0, questName);

            // I need to do this, strange though it may seem, to avoid certain complications
            if (questId == 0) questId = 1;
            setParam(ref journalFunctor, 1, questId);
            setParam(ref journalFunctor, 2, 1);

            return journalFunctor;
            }

        /// <summary>
        /// Sets the advancement of the quest
        /// </summary>
        /// <param name="questName">The name of the quest</param>
        /// <param name="questId">The id of the quset</param>
        /// <returns>The script funktor</returns>
        private static NWN2ScriptFunctor advanceQuest(string questName, int questId)
            {
            NWN2ScriptFunctor setReqNumber = new NWN2ScriptFunctor();
            setReqNumber.Script = makeScript("ga_global_int");
            String stringName = questNamePrep(questName);
            int setValue = questId;
            if (setValue == 0) setValue = 1;

            // I add the parameters
            setParam(ref setReqNumber, 0, stringName);
            setParam(ref setReqNumber, 1, setValue.ToString());
            return setReqNumber;
            }

        /// <summary>
        /// Creates the conflict funktor
        /// </summary>
        /// <returns>The script funktor</returns>
        private static NWN2ScriptFunctor conflict()
            {
            // I create the functer
            NWN2ScriptFunctor attackFunctor = new NWN2ScriptFunctor();
            attackFunctor.Script = makeScript("ga_attack");
            // I add the parameters
            setParam(ref attackFunctor, 0, "");
            setParam(ref attackFunctor, 1, 0);
            return attackFunctor;
            }

        /// <summary>
        /// Creates a script to either take or give a number of items
        /// </summary>
        /// <param name="give">Whether this should be a give script or a take script</param>
        /// <param name="ItemTag">The tag of the item</param>
        /// <param name="takeNumber">The number of items to take/give</param>
        /// <returns>The script</returns>
        private static NWN2ScriptFunctor giveOrTakeItem(bool give, string ItemTag, int takeNumber)
            {
            // I create the functor
            NWN2ScriptFunctor takeItemFunctor = new NWN2ScriptFunctor();

            String giveTakeScript = "ga_give_item";

            if (!give)
                {
                giveTakeScript = "ga_take_item";
                }
            takeItemFunctor.Script = makeScript(giveTakeScript);

            // I add the parameters
            // The tag of the item I want to check for
            setParam(ref takeItemFunctor, 0, ItemTag);

            // The number of elements
            setParam(ref takeItemFunctor, 1, takeNumber);

            // Whatever I want to check all the party members
            setParam(ref takeItemFunctor, 2, 0);

            return takeItemFunctor;
            }

        /// <summary>
        /// Creates a script that either gives or takes an amount of gold
        /// </summary>
        /// <param name="give">Whether the script should give (true) or take (false)</param>
        /// <param name="amount">The amount to give/take</param>
        /// <returns>The script</returns>
        private static NWN2ScriptFunctor giveOrTakeGold(bool give, int amount)
            {
            // I create the functor
            NWN2ScriptFunctor takeGoldFunctor = new NWN2ScriptFunctor();


            String scriptName = "ga_give_gold";
            if (!give)
                {
                scriptName = "ga_take_gold";
                }
            takeGoldFunctor.Script = makeScript(scriptName);

            // I add the parameters
            // The tag of the item I want to check for
            setParam(ref takeGoldFunctor, 0, amount);

            // Whatever I want to check all the party members
            setParam(ref takeGoldFunctor, 1, 0);
            return takeGoldFunctor;
            }

        /// <summary>
        /// Returns a script that either makes the player begin a explore or escort quest
        /// </summary>
        /// <param name="triggerHappens">Whether it is a escort or explore mission</param>
        /// <param name="convHappens">Whether it should be a escort or explore quest</param>
        /// <param name="actor">The actor that gives the conversation</param>
        /// <param name="trigger">The trigger that will mark the end of </param>
        /// <param name="Name">The name of the story node</param>
        /// <returns>The script functor</returns>
        private static NWN2ScriptFunctor EscortExplore(convType triggerHappens, convType convHappens, Actor actor, Actor trigger, String Name)
            {
            String tempStringName = "";
            String tempStringValue = "";
            NWN2ScriptFunctor EscortExploreFunctor = new NWN2ScriptFunctor();
            if (triggerHappens != convType.None)
                {
                if (convHappens == convType.Escort)
                    {
                    tempStringName = "sCreatureToEscort";
                    tempStringValue = actor.Tag;
                    }
                else if (convHappens == convType.Explore)
                    {
                    tempStringName = "sTriggerToFind";
                    tempStringValue = trigger.Tag;
                    }

                if (tempStringName != "")
                    {
                    EscortExploreFunctor.Script = makeScript("ga_global_string");

                    // I add the parameters
                    setParam(ref EscortExploreFunctor, 0, tempStringName);
                    setParam(ref EscortExploreFunctor, 1, tempStringValue);
                    }
                else
                    {
                    throw new Exception("There is something wrong in " + Name + " - go back and check its escort/explore values");
                    }
                }
            return EscortExploreFunctor;
            }

        /// <summary>
        /// Creates a script that adds an actor to the players party
        /// </summary>
        /// <param name="actor">The actor to add as a henchman to the players party</param>
        /// <returns>The functor</returns>
        private static NWN2ScriptFunctor addHenchMan(Actor actor)
            {
            //(happensEnum == EnumTypes.happens.Conv && convHappens == convType.Escort)

            // I create the functor
            NWN2ScriptFunctor addHenchManFunctor = new NWN2ScriptFunctor();
            addHenchManFunctor.Script = makeScript("ga_henchman_add");
            // I add the parameters

            // The henchman I want to add
            setParam(ref addHenchManFunctor, 0, actor.Tag);

            // Whatever I want to check all the party members
            setParam(ref addHenchManFunctor, 1, 1);

            // The tag of the master - not used since we want it to be the PC
            setParam(ref addHenchManFunctor, 2, "");

            // I make the henchman a real henchman
            setParam(ref addHenchManFunctor, 3, 1);

            return addHenchManFunctor;
            }

        /// <summary>
        /// Creates a script that removes a henchman from the players party
        /// </summary>
        /// <param name="actor">The actor to remove</param>
        /// <returns>The script functor</returns>
        private static NWN2ScriptFunctor removeHenchMan(Actor actor)
            {
            //(happensEnum == EnumTypes.happens.Conv && convHappens == convType.Escort)

            // I create the functor
            NWN2ScriptFunctor addHenchManFunctor = new NWN2ScriptFunctor();
            addHenchManFunctor.Script = makeScript("ga_henchman_remove");

            // I add the parameters
            // The tag of the henchman we want to remove
            setParam(ref addHenchManFunctor, 0, actor.Tag);

            // Not used, so I will not set it
            //     setParam(ref addHenchManFunctor, 1, "0");

            return addHenchManFunctor;
            }

        /// <summary>
        /// Returns a script that gives xp to the player
        /// </summary>
        /// <param name="xp">The amout of xp to give to the player</param>
        /// <returns>The script</returns>
        private static NWN2ScriptFunctor XP(int xp)
            {
            NWN2ScriptFunctor giveXpFunctor = new NWN2ScriptFunctor();
            giveXpFunctor.Script = makeScript("ga_give_xp");

            // I add the parameters
            setParam(ref giveXpFunctor, 0, xp);
            setParam(ref giveXpFunctor, 1, 0);
            return giveXpFunctor;
            }
        #endregion

        #region parameterSetters
        /// <summary>
        /// Sets the parameters of a conditional functor - with a int value
        /// </summary>
        /// <param name="fun">The conditional functor</param>
        /// <param name="paramNumber">The parameter number</param>
        /// <param name="value">The value of the parameter</param>
        private static void setParam(ref NWN2ConditionalFunctor fun, int paramNumber, int value)
            {
            fun.Parameters.Add(new NWN2ScriptParameter());
            fun.Parameters[paramNumber].ParameterType = NWN2ScriptParameterType.Int;
            fun.Parameters[paramNumber].ValueInt = value;
            }

        /// <summary>
        /// Sets the parameters of a conditional functor - with a String value
        /// </summary>
        /// <param name="fun">The conditional functor</param>
        /// <param name="paramNumber">The parameter number</param>
        /// <param name="value">The value of the parameter</param>
        private static void setParam(ref NWN2ConditionalFunctor fun, int paramNumber, string value)
            {
            fun.Parameters.Add(new NWN2ScriptParameter());
            fun.Parameters[paramNumber].ParameterType = NWN2ScriptParameterType.String;
            fun.Parameters[paramNumber].ValueString = value;
            }

        /// <summary>
        /// Sets the parameters of a script functor - with a int value
        /// </summary>
        /// <param name="fun">The script functor</param>
        /// <param name="paramNumber">The parameter number</param>
        /// <param name="value">The value of the parameter</param>
        private static void setParam(ref NWN2ScriptFunctor fun, int paramNumber, int value)
            {
            fun.Parameters.Add(new NWN2ScriptParameter());
            fun.Parameters[paramNumber].ParameterType = NWN2ScriptParameterType.Int;
            fun.Parameters[paramNumber].ValueInt = value;
            }

        /// <summary>
        /// Sets the parameters of a script functor - with a string value
        /// </summary>
        /// <param name="fun">The script functor</param>
        /// <param name="paramNumber">The parameter number</param>
        /// <param name="value">The value of the parameter</param>
        private static void setParam(ref NWN2ScriptFunctor fun, int paramNumber, string value)
            {
            fun.Parameters.Add(new NWN2ScriptParameter());
            fun.Parameters[paramNumber].ParameterType = NWN2ScriptParameterType.String;
            fun.Parameters[paramNumber].ValueString = value;
            }
        #endregion

        #region NWN2Conditional

        /// <summary>
        /// Sets the conditional values to ensure that a conversation will only fire when needed
        /// </summary>
        /// <param name="questName">The name of the quest</param>
        /// <returns>The functor</returns>
        private NWN2ConditionalFunctor conditional(string questName)
            {
            //I set the parameters of the condition
            NWN2ConditionalFunctor conditionalFunctor = null;
            if (preReqNode != null)
                {
                /* If we have the simple prerequisite, then the variable must be greater-or-equal to fire.
                 * So, since there is no built in greater-or-equal, I just apply the fact that greater-or-equal 
                 * is the same as the negatation of less-than 
                 */
                String condValue = preReqNode.questId.ToString();
                if (preReq == EnumTypes.prereq.SimplePrereq)
                    {
                    condValue = "<" + condValue;
                    }
                conditionalFunctor = makeJournalCheck(questName, condValue);
                conditionalFunctor.Not = preReq == EnumTypes.prereq.SimplePrereq;
                }
            else if (happensEnum == EnumTypes.happens.Trigger && triggerHappens != convType.None)
                {
                // If we have either an escort mission or an explore mission, I need to check if the value is correct
                conditionalFunctor = new NWN2ConditionalFunctor();

                String tempStringName = "";
                String tempStringValue = "";

                if (triggerHappens == convType.Escort)
                    {
                    tempStringName = "sCreatureToEscort";
                    tempStringValue = actor.Tag;
                    }
                else if (triggerHappens == convType.Explore)
                    {
                    tempStringName = "sTriggerToFind";
                    tempStringValue = trigger.Tag;
                    }
                if (tempStringName != "")
                    {
                    conditionalFunctor.Script = makeScript("gc_global_string");

                    setParam(ref conditionalFunctor, 0, tempStringName);
                    setParam(ref conditionalFunctor, 1, tempStringValue);
                    }
                }
            return conditionalFunctor;
            }


        private static NWN2ConditionalFunctor makeJournalCheck(string questName, string condValue)
            {

            NWN2ConditionalFunctor conditionalFunctor = new NWN2ConditionalFunctor();
            // I make the journal check
            conditionalFunctor.Script = makeScript("gc_journal_entry");
            questName = "Q_" + questNamePrep(questName);

            // I add the name of the quest
            setParam(ref conditionalFunctor, 0, questName);
            setParam(ref conditionalFunctor, 1, condValue);
            return conditionalFunctor;
            }

        /// <summary>
        /// Creates a functor which will check the progression of a quest
        /// </summary>
        /// <param name="questName">The name of the quest</param>
        /// <param name="questId">The id of the quest</param>
        /// <returns>The functor</returns>
        private static NWN2ConditionalFunctor intCheck(String questName, int questId)
            {
            NWN2ConditionalFunctor globalIntCheck = new NWN2ConditionalFunctor();
            globalIntCheck.Script = makeScript("gc_global_int");
            int letValue = questId;
            if (letValue == 0)
                letValue = 1;
            questName = questNamePrep(questName);
            setParam(ref globalIntCheck, 0, questName);
            setParam(ref globalIntCheck, 1, "<" + letValue);
            return globalIntCheck;
            }

        /// <summary>
        /// Creates a functor to check whether the player has a certian amount of gold
        /// </summary>
        /// <param name="amount">The amount of gold to remove</param>
        /// <returns>The functor</returns>
        private static NWN2ConditionalFunctor checkGold(int amount)
            {
            NWN2ConditionalFunctor goldCond = new NWN2ConditionalFunctor();
            goldCond.Script = makeScript("gc_check_gold");

            setParam(ref goldCond, 0, amount);
            setParam(ref goldCond, 1, 0);
            return goldCond;
            }

        /// <summary>
        /// Creates a node that checks whether the player has a certain item
        /// </summary>
        /// <param name="tag">The tag of the item to check for</param>
        /// <returns>The functor</returns>
        private static NWN2ConditionalFunctor checkItem(String tag)
            {
            NWN2ConditionalFunctor itemCond = new NWN2ConditionalFunctor();
            itemCond.Script = makeScript("gc_check_item");

            setParam(ref itemCond, 0, tag);
            setParam(ref itemCond, 1, 0);
            return itemCond;
            }

        /// <summary>
        /// Creates a functor which will check whether it is the 0 node
        /// </summary>
        /// <returns>The functor</returns>
        private static NWN2ConditionalFunctor node()
            {
            NWN2ConditionalFunctor node = new NWN2ConditionalFunctor();
            node.Script = makeScript("gc_node");

            // I add the parameters
            setParam(ref node, 0, 1);
            return node;
            }
        #endregion

        #region Properties
        public Actor itemVillian
            {
            get
                {
                return villianItem;
                }
            set
                {
                villianItem = value;
                }
            }

        public Actor itemGive
            {
            get
                {
                return giveItem;
                }
            set
                {
                giveItem = value;
                }
            }

        public Actor itemTake
            {
            get
                {
                return takeItem;
                }
            set
                {
                takeItem = value;
                }
            }

        public string Name
            {
            get
                {
                return name;
                }
            set
                {
                name = value;
                }
            }
        #endregion

        #region Utility methods
        /// <summary>
        /// The utility method which takes care of settig the trigger correctly
        /// </summary>
        /// <param name="actor">The actor who might be used in the script</param>
        /// <param name="trigger">The trigger we are going to work on</param>
        /// <param name="cConversation">The conversation which will be referenced by the trigger</param>
        /// <param name="triggerHappens">The type of trigger</param>
        private void makeEscort(string actorName, Actor trigger, string convName, string questName, int questId)
            {
            var varTable = trigger.Var;
            if (varTable == null)
                {
                varTable = new NWN2ScriptVarTable();
                }
            LinkedList<String> toRemove = new LinkedList<string>();
            toRemove.AddLast("Conversation");
            toRemove.AddLast("Node");
            toRemove.AddLast("Run");
            toRemove.AddLast("TalkNow");
            toRemove.AddLast("CutsceneBars");
            toRemove.AddLast("OnceOnly");
            toRemove.AddLast("MultiUse");
            toRemove.AddLast("CombatCutsceneSetup");
            toRemove.AddLast("NPC_Tag");

            var Conversation = new NWN2ScriptVariable("Conversation", convName);
            var Node = new NWN2ScriptVariable("Node", 1);
            var Run = new NWN2ScriptVariable("Run", 1);
            var TalkNow = new NWN2ScriptVariable("TalkNow", 1);
            var CutsceneBars = new NWN2ScriptVariable("CutsceneBars", 0);
            var OnceOnly = new NWN2ScriptVariable("OnceOnly", 0);
            var MutilUse = new NWN2ScriptVariable("MultiUse", 1);
            var CombatCutsceneSetup = new NWN2ScriptVariable("CombatCutsceneSetup", 0);
            var NPCTag = new NWN2ScriptVariable("NPC_Tag", actorName);

            // I add the variables to the tables
            varTable = removeDuplicates(varTable, toRemove);

            varTable.Add(Conversation);
            varTable.Add(Node);
            varTable.Add(Run);
            varTable.Add(TalkNow);
            varTable.Add(CutsceneBars);
            varTable.Add(OnceOnly);
            varTable.Add(MutilUse);
            varTable.Add(CombatCutsceneSetup);
            varTable.Add(NPCTag);
            
            condAdvance(trigger, varTable, questName, questId, 0, "gtr_speak_node", "");
            }

        /// <summary>
        /// Transforms a string into a OEIExoLocString with the same information
        /// </summary>
        /// <param name="sInput">The string that is to be copied</param>
        /// <param name="lang">The current Language</param>
        /// <param name="gender">The current gender</param>
        /// <returns>The OEIExoLocString with the information from the str, language and gender</returns>
        private static OEIExoLocString StringtoOEIE(string sInput, BWLanguages.BWLanguage lang, BWLanguages.Gender gender)
            {
            OEIExoLocString str = new OEIExoLocString();
            OEIExoLocSubString item = new OEIExoLocSubString();
            item.Language = lang;
            item.Gender = gender;
            item.Value = sInput;
            str.Strings.Add(item);
            return str;
            }

        /// <summary>
        /// Static utility method to copy a story node
        /// </summary>
        /// <param name="sn">The story node to be copied</param>
        /// <returns>The copied story node</returns>
        public static StoryNode Copy(StoryNode sn)
            {
            return (StoryNode)sn.MemberwiseClone();
            }

        /// <summary>
        /// Sets the module that the story node is connected to
        /// </summary>
        public static void setModule()
            {
            module = NWN2ToolsetMainForm.App.Module;
            }

        /// <summary>
        /// Fetches the script indicated
        /// </summary>
        /// <param name="scriptName">The name of the script that is to be fetched</param>
        /// <returns>The script</returns>
        private static IResourceEntry makeScript(string scriptName)
            {
            string scriptsPath = Path.Combine(OEIShared.IO.ResourceManager.Instance.BaseDirectory, @"Data\Scripts.zip");

            ResourceRepository scripts = (ResourceRepository)OEIShared.IO.ResourceManager.Instance.GetRepositoryByName(scriptsPath);
            ushort ncs1 = BWResourceTypes.GetResourceType("NSS");
            OEIResRef resRef = new OEIResRef(scriptName);
            IResourceEntry entry = scripts.FindResource(resRef, ncs1);
            NWN2GameScript script = new NWN2GameScript(entry);
            script.Demand();
            return script.Resource;
            }

        private static void debug(String text)
            {
            Console.WriteLine(text);
            }

        override public string ToString()
            {
            String stringValue = name;
            if (endPoint)
                {
                stringValue = name + " (endpoint)";
                }
            return stringValue;
            }
        #endregion


        private static NWN2GameScript getAdvanceJournal()
            {
            NWN2GameScript advanceScript = module.Scripts["ga_advance_journal"];
            if (advanceScript == null)
                {
                advanceScript = new NWN2GameScript("ga_advance_journal", module.Repository.DirectoryName, module.Repository);
                advanceScript.Data = Properties.Resources.ga_advance_journal;
                advanceScript.OEISerialize();
                module.Scripts.Add(advanceScript);
                }
            return advanceScript;
            }
        }
    }