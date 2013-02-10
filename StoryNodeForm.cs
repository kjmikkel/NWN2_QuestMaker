using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NWN2Toolset.NWN2.Data;
using NWN2Toolset.NWN2.Data.Blueprints;

namespace QuestMaker
    {
    public partial class StoryNodeForm : Form
        {
        #region Fields
        private Actor giver;
        private Actor villian;
        private NWN2TriggerBlueprint trigger;

        private LinkedList<Actor> extra;
        private LinkedList<Actor> props;

        private LinkedList<Actor> creatures;
        private LinkedList<Actor> doors;
        private LinkedList<Actor> placeables;

        private LinkedList<Actor> triggers;
        private NWN2GameModule module;

        private StoryNode sNode;
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="giver"></param>
        /// <param name="villian"></param>
        /// <param name="extra"></param>
        /// <param name="props"></param>
        /// <param name="prevStoryNodes"></param>
        /// <param name="triggers"></param>
        /// <param name="module"></param>
        private void basicSetup(Actor giver, Actor villian, LinkedList<Actor> extra, LinkedList<Actor> props, LinkedList<StoryNode> prevStoryNodes, LinkedList<Actor> triggers, NWN2GameModule module)
            {
            this.giver = giver;
            this.villian = villian;
            this.extra = extra;
            this.props = props;
            this.module = module;
            this.triggers = triggers;

            creatures = loadActors(EnumTypes.actorType.Creature);
            placeables = loadActors(EnumTypes.actorType.Placeable);
            doors = loadActors(EnumTypes.actorType.Door);
            if (creatures.Count > 0)
                {
                comboActorType.Items.Add("Creatures");
                }

            if (placeables.Count > 0)
                {
                comboActorType.Items.Add("Placeables");
                }

            if (doors.Count > 0)
                {
                comboActorType.Items.Add("Doors");
                }

            comboActorType.SelectedIndex = 0;
            changeActors();

            // I add the props
            foreach (Actor item in props)
                {
                comboTakeItem.Items.Add(item);
                comboGiveItem.Items.Add(item);
                comboDropItem.Items.Add(item);
                }

            if (comboTakeItem.Items.Count > 0)
                {
                debug("selectedIndex");
                comboTakeItem.SelectedIndex = 0;
                comboGiveItem.SelectedIndex = 0;
                comboDropItem.SelectedIndex = 0;
                }

            // I insert the previous story nodes
            foreach (StoryNode sNode in prevStoryNodes)
                {
                comboPreRec.Items.Add(sNode);
                }

            if (prevStoryNodes.Count == 0)
                {
                radioSimpPreReq.Enabled = false;
                radioCastSpecPreReq.Enabled = false;
                }
            else
                {
                comboPreRec.SelectedIndex = 0;
                }

            foreach (Actor trig in triggers)
                {
                comboTriggers.Items.Add(trig);
                }

            if (comboTriggers.Items.Count > 0)
                {
                comboTriggers.SelectedIndex = 0;
                }
            else
                {
                checkEscort.Enabled = false;
                checkExplore.Enabled = false;
                triggerRadio.Enabled = false;
                }
            }

        /// <summary>
        /// We do not yet have a storyNode
        /// </summary>
        /// <param name="giver">The current giver</param>
        /// <param name="villian">The current villian</param>
        /// <param name="extra">The current extras</param>
        /// <param name="props">The current items</param>
        /// <param name="prevStoryNodes">The previous story nodes</param>
        /// <param name="triggers">The current triggers</param>
        /// <param name="module">The module we are using</param>
        public StoryNodeForm(Actor giver, Actor villian, LinkedList<Actor> extra, LinkedList<Actor> props, LinkedList<StoryNode> prevStoryNodes, LinkedList<Actor> triggers, NWN2GameModule module)
            {
            InitializeComponent();
            basicSetup(giver, villian, extra, props, prevStoryNodes, triggers, module);
            if (prevStoryNodes.Count > 0)
                {
                radioNoPreReq.Checked = false;
                radioSimpPreReq.Checked = true;
                }
            }

        // 
        /// <summary>
        /// We have a story node
        /// </summary>
        /// <param name="sNode">The story node we are editing</param>
        /// <param name="giver">The current giver</param>
        /// <param name="villian">The current villian</param>
        /// <param name="extra">The current extras</param>
        /// <param name="props">The current items</param>
        /// <param name="prevStoryNodes">The previous story nodes</param>
        /// <param name="triggers">The current triggers</param>
        /// <param name="module">The module we are using</param>
        public StoryNodeForm(StoryNode sNode, Actor giver, Actor villian, LinkedList<Actor> extra, LinkedList<Actor> props, LinkedList<StoryNode> prevStoryNodes, LinkedList<Actor> triggers, NWN2GameModule module)
            {
            InitializeComponent();
            storyNodeName.Text = sNode.Name;

            basicSetup(giver, villian, extra, props, prevStoryNodes, triggers, module);
            comboActor.SelectedItem = sNode.actor;


            if (comboActor.SelectedItem == null && comboActor.Items.Count > 0)
                {
                comboActor.SelectedIndex = 0;
                }

            comboTriggers.SelectedItem = trigger;

            // Look at this null comparison to see if it is correct
            if (comboTriggers.SelectedItem == null && comboTriggers.Items.Count > 0)
                {
                comboTriggers.SelectedIndex = 0;
                }

            // I add the data to the "What happens" field
            switch (sNode.happensEnum)
                {
                case EnumTypes.happens.Conv:
                    radioConvs.Checked = true;
                    break;

                case EnumTypes.happens.Conflict:
                    radioVillian.Checked = true;
                    break;

                case EnumTypes.happens.OpenDoor:
                    radioDoor.Checked = true;
                    break;

                case EnumTypes.happens.Trigger:
                    triggerRadio.Checked = true;
                    break;
                }

            /* I set options (radiobuttons and checkbuttons)
            These are independant of the above as the user may fiddle around
             */
            // Conv: I set the escort/explore options
            checkExplore.Checked = (sNode.convHappens == StoryNode.convType.Explore);
            checkEscort.Checked = (sNode.convHappens == StoryNode.convType.Escort);

            // Villian
            checkVillianTalk.Checked = sNode.villianTalk;
            checkVillianItem.Checked = (sNode.villianGotItem && sNode.actor.type == EnumTypes.actorType.Creature);

            //Door or placable:
            checkContainerContains.Checked = (sNode.villianGotItem && sNode.actor.type == EnumTypes.actorType.Placeable);

            // Trigger:          
            radioFinishEscort.Checked = (sNode.triggerHappens == StoryNode.convType.Escort);
            radioFinishExplore.Checked = (sNode.triggerHappens == StoryNode.convType.Explore);
            triggerPanel.Enabled = (sNode.happensEnum == EnumTypes.happens.Trigger);

            // I add the data to the "Conversation Type" field

            #region Conversation Type
            switch (sNode.convEnum)
                {
                case EnumTypes.conv.QuestInfo:
                    radioQuestInfo.Checked = true;
                    break;

                case EnumTypes.conv.Single:
                    radioSingleStatement.Checked = true;
                    break;

                case EnumTypes.conv.None: break;
                }
            #endregion

            #region Items
            if ((sNode.giveItem != null) || (sNode.giveGold > 0))
                {
                comboGiveItem.SelectedItem = sNode.giveItem;
                giveItemNumber.Value = sNode.takeNumber;
                giveGold.Text = sNode.giveGold.ToString();
                checkGive.Checked = true;
                }

            if ((sNode.takeItem != null) || (sNode.takeGold > 0))
                {
                comboTakeItem.SelectedItem = sNode.takeItem;
                takeItemNumber.Value = sNode.takeNumber;
                takeGold.Text = sNode.takeGold.ToString();
                checkTake.Checked = true;
                }

            if (sNode.villianItem != null)
                {
                comboDropItem.SelectedItem = sNode.villianItem;
                }
            #endregion

            #region convesation
            greetBox.Text = sNode.greeting;
            acceptBox.Text = sNode.acceptence;
            actionBox.Text = sNode.action;
            rejectBox.Text = sNode.rejection;
            #endregion

            #region Prerequisite
            switch (sNode.preReq)
                {
                case EnumTypes.prereq.NoPrereq: radioNoPreReq.Checked = true;
                    break;

                case EnumTypes.prereq.SimplePrereq: radioSimpPreReq.Checked = true;
                    //        if(prevStoryNodes.Contains(sNode.preReq) {
                    comboPreRec.SelectedItem = sNode.preReqNode;
                    //      }
                    break;

                case EnumTypes.prereq.CastSpecificPrereq: radioCastSpecPreReq.Checked = true;
                    //  if(prevStoryNodes.Contains(sNode.preReq) {
                    comboPreRec.SelectedItem = sNode.preReqNode;
                    //  }
                    break;
                }
            #endregion

            // Journal field
            JournalBox.Text = sNode.journal;
            checkJournal.Checked = sNode.journalCheck;

            // EndPoint?
            checkEndPoint.Checked = sNode.endPoint;

            XP.Text = sNode.xp.ToString();
            }
        #endregion

        #region CheckChanged
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void convsRadio_CheckedChanged(object sender, EventArgs e)
            {
            convPanel.Enabled = true;
            panelItem.Enabled = radioConvs.Checked;
            convTypePanel.Enabled = radioConvs.Checked;
            EscortExplorePanel.Enabled = radioConvs.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confVillian_CheckedChanged(object sender, EventArgs e)
            {
            // The options
            checkVillianTalk.Enabled = radioVillian.Checked;
            checkVillianItem.Enabled = radioVillian.Checked;

            comboDropItem.Enabled = radioVillian.Checked && checkVillianItem.Checked;
            panelItem.Enabled = radioVillian.Checked && checkVillianItem.Checked;

            convPanel.Enabled = checkVillianTalk.Checked;
            convTypePanel.Enabled = checkVillianTalk.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
            {
            triggerPanel.Enabled = triggerRadio.Checked;
            raFinishEscortEffect();
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestInfo_CheckedChanged(object sender, EventArgs e)
            {
            talkBoxChange(radioQuestInfo.Checked);
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void singleStatement_CheckedChanged(object sender, EventArgs e)
            {
            talkBoxChange(!radioSingleStatement.Checked);
            }

        /// <summary>
        /// A gasket method to make sure the boxes have the correct values
        /// </summary>
        private void raFinishEscortEffect()
            {
            Boolean value = (radioFinishEscort.Checked || radioFinishExplore.Checked) && triggerRadio.Checked;
      //      panelItem.Enabled = value;
       //     convPanel.Enabled = value;

            talkBoxChange(!value);

            comboTriggers.Enabled = triggerRadio.Checked;
            }

        private void talkBoxChange(bool change)
            {
            greetBox.Enabled = true;
            acceptBox.Enabled = change;
            actionBox.Enabled = change;
            rejectBox.Enabled = change;
             
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void takeCheck_CheckedChanged(object sender, EventArgs e)
            {
            comboTakeItem.Enabled = checkTake.Checked;
            takeItemNumber.Enabled = checkTake.Checked;
            takeGold.Enabled = checkTake.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void giveCheck_CheckedChanged(object sender, EventArgs e)
            {
            comboGiveItem.Enabled = checkGive.Checked;
            giveItemNumber.Enabled = checkGive.Checked;
            giveGold.Enabled = checkGive.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void villianTalk_CheckedChanged(object sender, EventArgs e)
            {
            convPanel.Enabled = checkVillianTalk.Checked;
            convTypePanel.Enabled = checkVillianTalk.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void noPreReq_CheckedChanged(object sender, EventArgs e)
            {
            comboPreRec.Enabled = false;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void simpPreReq_CheckedChanged(object sender, EventArgs e)
            {
            comboPreRec.Enabled = radioSimpPreReq.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void castSpecPreReq_CheckedChanged(object sender, EventArgs e)
            {
            comboPreRec.Enabled = radioCastSpecPreReq.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void villianItem_CheckedChanged(object sender, EventArgs e)
            {
            comboDropItem.Enabled = checkVillianItem.Checked;
            panelItem.Enabled = checkVillianItem.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void journalCheck_CheckedChanged(object sender, EventArgs e)
            {
            JournalBox.Enabled = checkJournal.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void takeCheck_EnabledChanged(object sender, EventArgs e)
            {
            comboTakeItem.Enabled = checkTake.Enabled && checkTake.Checked;
            takeGold.Enabled = checkTake.Enabled && checkTake.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void giveCheck_EnabledChanged(object sender, EventArgs e)
            {
            comboGiveItem.Enabled = checkGive.Enabled && checkTake.Checked;
            giveGold.Enabled = checkGive.Enabled && checkTake.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void convTypePanel_EnabledChanged(object sender, EventArgs e)
            {
            //       takeCheck.Enabled = convTypePanel.Enabled;
            //       giveCheck.Enabled = convTypePanel.Enabled;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xpCheck_CheckedChanged(object sender, EventArgs e)
            {
            XP.Enabled = checkXp.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Explore_CheckedChanged(object sender, EventArgs e)
            {
            checkEscort.Enabled = !checkExplore.Checked;
            comboTriggers.Enabled = checkExplore.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Escort_CheckedChanged(object sender, EventArgs e)
            {
            checkExplore.Enabled = !checkEscort.Checked;
            comboTriggers.Enabled = checkEscort.Checked;
            }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void raFinishEscort_CheckedChanged(object sender, EventArgs e)
            {
            raFinishEscortEffect();
            }

        #endregion

        /// <summary>
        /// Gets all the information from the form and makes it into a story node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void finish_Click(object sender, EventArgs e)
            {
            #region Initial Checks
            if (storyNodeName.Text == String.Empty)
                {
                report("You need to give the story node a name");
                storyNodeName.Focus();
                return;
                }
            if (comboActor.SelectedItem == null)
                {
                report("You need to select an actor");
                comboActor.Focus();
                return;
                }
            if ((radioSimpPreReq.Checked || radioCastSpecPreReq.Checked) && comboPreRec.SelectedItem == null)
                {
                report("You need to choose a story node or choose to have no prerequisite");
                
                return;
                }

            // if either the journalCheck is disabled or the Journalbox is non-empty we continue
            if (checkJournal.Checked && JournalBox.Text == String.Empty)
                {
                report("You need to write something for the Journal entry");
                JournalBox.Focus();
                return;
                }
            #endregion

            // I create the story node
            sNode = new StoryNode(storyNodeName.Text, (Actor)comboActor.SelectedItem, (Actor)comboTriggers.SelectedItem, module);

            #region whatHappens
            // I set happens
            if (radioConvs.Checked)
                {
                sNode.happensEnum = EnumTypes.happens.Conv;
                }
            else if (radioVillian.Checked)
                {
                sNode.happensEnum = EnumTypes.happens.Conflict;
                sNode.villianTalk = checkVillianTalk.Checked;
                }
            else if (radioDoor.Checked)
                {
                sNode.happensEnum = EnumTypes.happens.OpenDoor;
                sNode.containerContains = checkContainerContains.Checked && checkContainerContains.Enabled;
                }
            else if (triggerRadio.Checked)
                {
                sNode.happensEnum = EnumTypes.happens.Trigger;
                }
            #endregion

            #region convType
            // I set the convType
            if (radioConvs.Checked || (radioVillian.Checked && checkVillianTalk.Checked) || radioDoor.Checked)
                {
                if (radioQuestInfo.Checked)
                    {
                    sNode.convEnum = EnumTypes.conv.QuestInfo;
                    }
                else
                    {
                    sNode.convEnum = EnumTypes.conv.Single;
                    }
                }

            // I set the type of the convTypeEnum (whatever the player has to escort/explore)
            if (checkEscort.Checked)
                {
                sNode.convHappens = StoryNode.convType.Escort;
                }
            else if (checkExplore.Checked)
                {
                sNode.convHappens = StoryNode.convType.Explore;
                }

            // I set whether the finish trigger is escort or Explore
            if (radioFinishEscort.Checked)
                {
                sNode.triggerHappens = StoryNode.convType.Escort;
                }
            else if (radioFinishExplore.Checked)
                {
                sNode.triggerHappens = StoryNode.convType.Explore;
                
                }
            #endregion

            #region Items
            // The Items

            bool end = false;
            if (checkTake.Checked && checkTake.Enabled)
                {
                if (comboTakeItem.SelectedItem != null && comboTakeItem.SelectedText.ToLower() != "nothing")
                    {
                    sNode.itemTake = (Actor)comboTakeItem.SelectedItem;
                    sNode.takeNumber = (int)takeItemNumber.Value;
                    debug("Take item: " + sNode.itemGive.ToString() + ", number: " + sNode.giveNumber);
                    }
                try
                    {
                    sNode.takeGold = int.Parse(takeGold.Text);
                    }
                catch //(Exception ex)
                    {
                    end = true;
                    report("You can only input integers");
                    }
                }

            if (checkGive.Checked && checkGive.Enabled)
                {

                if (comboGiveItem.SelectedItem != null && comboTakeItem.SelectedText.ToLower() != "nothing")
                    {
                    sNode.itemGive = (Actor)comboGiveItem.SelectedItem;
                    sNode.giveNumber = (int)giveItemNumber.Value;
                    debug("Give item: " + sNode.itemGive.ToString() + ", number: " + sNode.giveNumber);
                    }
                try
                    {
                    sNode.giveGold = int.Parse(giveGold.Text);
                    }
                catch// (Exception ex)
                    {
                    end = true;
                    report("You can only input integers");
                    }
                }

            // The villian part
            if (
                //(villianItem.Checked || containerContains.Checked) && 
                //  villianItem.Enabled 
                comboDropItem.Enabled && comboDropItem.SelectedItem != null && comboDropItem.Text.ToLower() != "nothing")
                {
                //           System.Windows.Forms.report("Got item: " + ((Actor)dropItemCombo.SelectedItem).ToString());
                sNode.villianItem = (Actor)comboDropItem.SelectedItem;
                sNode.villianGotItem = true;
                }
            #endregion Items

            #region Conversation
            // The conversation
            // For there to be an conversation, the conv options only needs to be enabled
            if (radioQuestInfo.Enabled)
                {
                sNode.greeting = greetBox.Text;

                if (radioQuestInfo.Checked)
                    {
                    sNode.acceptence = acceptBox.Text;
                    sNode.action = actionBox.Text;
                    sNode.rejection = rejectBox.Text;
                    }
                }

            if (triggers.Count > 0 && comboTriggers.Enabled == true)
                {
                trigger = (NWN2TriggerBlueprint)((Actor)comboTriggers.SelectedItem).blueprint;
                }
            #endregion

            #region PreRec
            sNode.preReq = EnumTypes.prereq.NoPrereq;
            if (comboPreRec.SelectedItem != null && !radioNoPreReq.Checked)
                {
                sNode.preReqNode = (StoryNode)comboPreRec.SelectedItem;

                if (radioSimpPreReq.Checked)
                    {
                    sNode.preReq = EnumTypes.prereq.SimplePrereq;
                    }
                else
                    {
                    sNode.preReq = EnumTypes.prereq.CastSpecificPrereq;
                    }
                }
            #endregion

            #region Journal and XP
            /* I add the journal text - if there is no text I will not add a
         * journal entry, nor will I use it as prerequiste
         */
            sNode.journal = JournalBox.Text;
            sNode.journalCheck = checkJournal.Checked;

            //.. and the xp

            try
                {
                sNode.xp = int.Parse(XP.Text);
                if (sNode.xp < 0)
                    {
                    report("XP must be given as a nonnegative value");
                    return;
                    }
                }
            catch
                {
                end = true;
                report("You can only input integers");
                return;
                }

            sNode.endPoint = checkEndPoint.Checked;

            if (!end)
                {
                DialogResult = DialogResult.OK;
                this.Close();
                }
            #endregion
            }

        /// <summary>
        /// Controls whether checkTake and checkGive should be enabled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void villianItem_EnabledChanged(object sender, EventArgs e)
            {
            /* I set the checkboxes
             * The && part is to replace a if-else statement
             */
            checkTake.Enabled = !checkVillianItem.Enabled;
            checkGive.Enabled = !checkVillianItem.Enabled;
            }

        /// <summary>
        /// The close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancel_Click(object sender, EventArgs e)
            {
            this.Close();
            }


        /// <summary>
        /// Returns the story node
        /// </summary>
        /// <returns>The story node</returns>
        public StoryNode getStoryNode()
            {
            return sNode;
            }

        /// <summary>
        /// If the door contian radio buttons value has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void doorContain_CheckedChanged(object sender, EventArgs e)
            {
            checkContainerContains.Enabled = radioDoor.Checked && comboActor.SelectedItem != null &&
                ((Actor)(comboActor.SelectedItem)).type == EnumTypes.actorType.Placeable;
            containerCheck();
            convTypePanel.Enabled = !radioDoor.Enabled;
            convPanel.Enabled = !radioDoor.Enabled;
            }

        private void containerContains_CheckedChanged(object sender, EventArgs e)
            {
            containerCheck();
            }

        private void containerCheck()
            {
            panelItem.Enabled = checkContainerContains.Checked;
            comboDropItem.Enabled = checkContainerContains.Checked;
            checkTake.Enabled = false;
            checkGive.Enabled = false;
            }

        /// <summary>
        /// Changes the type of actors available in the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActorType_SelectedValueChanged(object sender, EventArgs e)
            {
            changeActors();
            }

        /// <summary>
        /// Updates the actors available in the combo box to the type specified
        /// </summary>
        private void changeActors()
            {
            String value = comboActorType.Text;
            comboActor.Items.Clear();
            if (value == "Creatures")
                {
                foreach (Actor act in creatures)
                    {
                    comboActor.Items.Add(act);
                    }

                radioVillian.Enabled = true;

                if (comboTriggers.Items.Count > 0)
                    checkEscort.Enabled = true;

                radioDoor.Enabled = false;
                checkContainerContains.Enabled = false;
                }
            else if (value == "Doors" || value == "Placeables")
                {
                if (value == "Doors")
                    {
                    foreach (Actor act in doors)
                        {
                        comboActor.Items.Add(act);
                        }
                    }
                else
                    {
                    foreach (Actor act in placeables)
                        {
                        comboActor.Items.Add(act);
                        }
                    }

                radioVillian.Enabled = false;
                checkVillianItem.Enabled = false;
                checkVillianTalk.Enabled = false;
                checkEscort.Enabled = false;
                checkExplore.Enabled = true;
                radioDoor.Enabled = true;
                }

            comboActor.Text = String.Empty;
            if (comboActor.Items.Count > 0)
                comboActor.SelectedIndex = 0;
            }

        /// <summary>
        /// Returns a linked list with all the actors connected to the story node from a given type
        /// </summary>
        /// <param name="type">The type of actors that are to be returned</param>
        /// <returns>The linked list containing the acotr</returns>
        private LinkedList<Actor> loadActors(EnumTypes.actorType type)
            {
            LinkedList<Actor> actors = new LinkedList<Actor>();
            if (giver != null && giver.type == type)
                {
                actors.AddLast(giver);
                }

            if (villian != null && villian.type == type)
                {
                actors.AddLast(villian);
                }

            if (extra != null)
                {
                foreach (Actor act in extra)
                    {
                    if (act.type == type)
                        {
                        actors.AddLast(act);
                        }
                    }
                }

            return actors;
            }

        /// <summary>
        /// Report the string to the user
        /// </summary>
        /// <param name="str">The string to reprt</param>
        private static void report(String str)
            {
            MessageBox.Show(str);
            }

        private static void debug(String str)
            {
            Console.WriteLine(str);
            }
        }
    }