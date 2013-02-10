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

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NWN2Toolset.NWN2.Data.Blueprints;
using NWN2Toolset.NWN2.Data.Instances;
using NWN2Toolset;
using NWN2Toolset.NWN2.Data.Templates;
using OEIShared.IO;
using NWN2Toolset.NWN2.Data.Campaign;
using NWN2Toolset.NWN2.IO;
using OEIShared.Utils;
using NWN2Toolset.NWN2.Data.TypedCollections;
using OEIShared.IO.GFF;
using System.IO;
using NWN2Toolset.NWN2.Data;

namespace QuestMaker
    {
    public partial class ModifyNew : Form
        {

        static public Actor lastActor = null;
        static private bool boolInstance;
        static private bool edit;
        static public ModifyNew modifyNew;
        static private LinkedList<Actor> actorList = new LinkedList<Actor>();
        static private bool saveValue = false;

        /// <summary>
        /// The constructor
        /// </summary>
        private ModifyNew()
            {
            InitializeComponent();
            }

        /// <summary>
        /// The ModifyNew form is created for editing
        /// </summary>
        /// <param name="actor">The actor that will be edited</param>
        /// <param name="type">The type of actors that can be returned</param>
        /// <returns></returns>
        public static ModifyNew getModifyNew(Actor actor, EnumTypes.actorType type)
            {
            var mod = getModifyNew(type);
            mod.Text = "Edit blueprint";
            mod.Cancel.Enabled = false;
            edit = true;
            if (actor.boolInstance)
                {
                mod.propGrid.SelectedObjects = new object[] { actor.instance };
                boolInstance = true;
                }
            else
                {
                mod.propGrid.SelectedObjects = new object[] { actor.blueprint };
                boolInstance = false;
                }

            return mod;
            }

        /// <summary>
        /// The modifyNew form is created for creating new actors
        /// </summary>
        /// <param name="type">The type of actors that can be returned</param>
        /// <returns></returns>
        public static ModifyNew getModifyNew(EnumTypes.actorType type)
            {
            edit = false;
            saveValue = false;
            if (modifyNew == null)
                {
                modifyNew = new ModifyNew();
                debug("Create new modify");
                }
            modifyNew.Text = "Create new blueprint";
            modifyNew.comboType.Items.Clear();
            modifyNew.propGrid.SelectedObjects = null;
            modifyNew.propGrid.RefreshGrid();

            // I set which types can created
            if ((type & EnumTypes.actorType.Creature) == EnumTypes.actorType.Creature)
                modifyNew.comboType.Items.Add("Creature");

            if ((type & EnumTypes.actorType.Door) == EnumTypes.actorType.Door)
                modifyNew.comboType.Items.Add("Door");

            if ((type & EnumTypes.actorType.Placeable) == EnumTypes.actorType.Placeable)
                modifyNew.comboType.Items.Add("Placeable");

            if ((type & EnumTypes.actorType.Item) == EnumTypes.actorType.Item)
                modifyNew.comboType.Items.Add("Item");

            if ((type & EnumTypes.actorType.TriggerRegion) == EnumTypes.actorType.TriggerRegion)
                modifyNew.comboType.Items.Add("Trigger");

            if (modifyNew.comboType.Items.Count > 0)
                modifyNew.comboType.SelectedIndex = 0;

            return modifyNew;
            }

        /// <summary>
        /// Sets the lastActor value to the actor in the property pane, and closes the form
        /// </summary>
        private void OK_Click(object sender, EventArgs e)
            {
            // To flush any non saved data for the blueprint
            modifyNew.propGrid.RefreshGrid();

            saveValue = true;
            
            object data = propGrid.SelectedObjects[0];
            if (boolInstance)
                {
                if (data is NWN2CreatureInstance)
                    {
                    lastActor = new Actor((NWN2CreatureInstance)data, EnumTypes.actorType.Creature);
                    }
                else if (data is NWN2PlaceableInstance)
                    {
                    lastActor = new Actor((NWN2PlaceableInstance)data, EnumTypes.actorType.Placeable);
                    }
                else if (data is NWN2DoorInstance)
                    {
                    lastActor = new Actor((NWN2DoorInstance)data, EnumTypes.actorType.Door);
                    }
                else if (data is NWN2ItemInstance)
                    {
                    lastActor = new Actor((NWN2ItemInstance)data, EnumTypes.actorType.Item);
                    }
                else if (data is NWN2TriggerInstance)
                    {
                    lastActor = new Actor((NWN2TriggerInstance)data, EnumTypes.actorType.TriggerRegion);
                    }
                }
            else
                {
                if (data is NWN2CreatureBlueprint)
                    {
                    lastActor = new Actor((NWN2CreatureBlueprint)data, EnumTypes.actorType.Creature);
                    //    Console.WriteLine("Creature blueprint");
                    }
                else if (data is NWN2PlaceableBlueprint)
                    {
                    lastActor = new Actor((NWN2PlaceableBlueprint)data, EnumTypes.actorType.Placeable);
                    }
                else if (data is NWN2DoorBlueprint)
                    {
                    lastActor = new Actor((NWN2DoorBlueprint)data, EnumTypes.actorType.Door);
                    }
                else if (data is NWN2ItemBlueprint)
                    {
                    lastActor = new Actor((NWN2ItemBlueprint)data, EnumTypes.actorType.Item);
                    }
                else if (data is NWN2TriggerBlueprint)
                    {
                    lastActor = new Actor((NWN2TriggerBlueprint)data, EnumTypes.actorType.TriggerRegion);
                    }
                }
            DialogResult = DialogResult.OK;
            this.Close();
            }

        /// <summary>
        /// Closes the form and cleaning up the form
        /// </summary>
        private void Cancel_Click(object sender, EventArgs e)
            {
            removeBlueprintActor();
            this.Close();
            }

        /// <summary>
        /// Debug method
        /// </summary>
        /// <param name="str">The string to print to the console</param>
        private static void debug(String str)
            {
            Console.WriteLine(str);
            }

        /// <summary>
        /// Creatre a new blueprint
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
            {
            createNew();
            }

        /// <summary>
        /// Creates a new blueprint if there is only one type
        /// </summary>
        public void testCreate()
            {
            if (this.comboType.Items.Count == 1)
                {
                createNew();
                }
            }

        /// <summary>
        /// Create a new blueprint, depending on the combobox value
        /// </summary>
        private void createNew()
            {
            // We need to make sure that you can cancel the blueprint
            modifyNew.Cancel.Enabled = true;

            // If there is already a new blueprint has been made, then we delete the old one
            if (propGrid.SelectedObjects != null && propGrid.SelectedObjects.Length > 0 && !edit)
                {
                NWN2Toolset.NWN2ToolsetMainForm.App.Module.DeleteBlueprint((INWN2Blueprint)propGrid.SelectedObjects[0]);
                }

            if (comboType.Text == "Creature")
                {
                propGrid.SelectedObjects = new object[] { createNewBlueprint(NWN2ObjectType.Creature, NWN2BlueprintLocationType.Module) };
                }
            else if (comboType.Text == "Door")
                {
                propGrid.SelectedObjects = new object[] { createNewBlueprint(NWN2ObjectType.Door, NWN2BlueprintLocationType.Module) };
                }
            else if (comboType.Text == "Placeable")
                {
                propGrid.SelectedObjects = new object[] { createNewBlueprint(NWN2ObjectType.Placeable, NWN2BlueprintLocationType.Module) };
                }
            else if (comboType.Text == "Item")
                {
                propGrid.SelectedObjects = new object[] { createNewBlueprint(NWN2ObjectType.Item, NWN2BlueprintLocationType.Module) };
                }
            else if (comboType.Text == "Trigger")
                {
                propGrid.SelectedObjects = new object[] { createNewBlueprint(NWN2ObjectType.Trigger, NWN2BlueprintLocationType.Module) };
                }
            edit = false;
            }

        /// <summary>
        /// Create a new blueprint
        /// </summary>
        /// <param name="objectType">The type of the blueprint that is about to be created</param>
        /// <param name="locationType">The location where the blueprint will be placed</param>
        /// <returns>The new blueprint</returns>
        private static INWN2Blueprint createNewBlueprint(NWN2ObjectType objectType, NWN2BlueprintLocationType locationType)
            {
            INWN2Blueprint cBlueprint = null;
            IResourceRepository userOverrideDirectory = null;
            NWN2Campaign activeCampaign = NWN2CampaignManager.Instance.ActiveCampaign;
            INWN2BlueprintSet instance = null;
            switch (locationType)
                {
                case NWN2BlueprintLocationType.Global:
                    userOverrideDirectory = NWN2ResourceManager.Instance.UserOverrideDirectory;
                    if (userOverrideDirectory != null)
                        {
                        userOverrideDirectory = NWN2ResourceManager.Instance.OverrideDirectory;
                        }
                    instance = NWN2GlobalBlueprintManager.Instance;
                    break;

                case NWN2BlueprintLocationType.Module:
                    instance = NWN2Toolset.NWN2ToolsetMainForm.App.Module;
                    userOverrideDirectory = NWN2Toolset.NWN2ToolsetMainForm.App.Module.Repository;
                    break;

                case NWN2BlueprintLocationType.Campaign:
                    userOverrideDirectory = (activeCampaign != null) ? activeCampaign.Repository : null;
                    instance = activeCampaign;
                    break;

                default:
                    throw new Exception("Unknown object type in CreateNewBlueprint()" + locationType.ToString());
                }
           
            if ((userOverrideDirectory == null) || (instance == null))
                {
                return null;
                }
            switch (objectType)
                {
                case NWN2ObjectType.Creature:
                    cBlueprint = new NWN2CreatureBlueprint();
                    createHelp(cBlueprint, NWN2GlobalBlueprintManager.GetTemporaryBlueprintName(NWN2ObjectType.Creature, locationType), userOverrideDirectory, BWResourceTypes.GetResourceType("UTC"));
                    break;

                case NWN2ObjectType.Door:
                    cBlueprint = new NWN2DoorBlueprint();
                    createHelp(cBlueprint, NWN2GlobalBlueprintManager.GetTemporaryBlueprintName(NWN2ObjectType.Door, locationType), userOverrideDirectory, BWResourceTypes.GetResourceType("UTD"));
                    break;

                case NWN2ObjectType.Item:
                    cBlueprint = new NWN2ItemBlueprint();
                    createHelp(cBlueprint, NWN2GlobalBlueprintManager.GetTemporaryBlueprintName(NWN2ObjectType.Item, locationType), userOverrideDirectory, BWResourceTypes.GetResourceType("UTI"));
                    break;

                case NWN2ObjectType.Placeable:
                    cBlueprint = new NWN2PlaceableBlueprint();
                    createHelp(cBlueprint, NWN2GlobalBlueprintManager.GetTemporaryBlueprintName(NWN2ObjectType.Placeable, locationType), userOverrideDirectory, BWResourceTypes.GetResourceType("UTP"));
                    break;

                case NWN2ObjectType.Trigger:
                    cBlueprint = new NWN2TriggerBlueprint();
                    createHelp(cBlueprint, NWN2GlobalBlueprintManager.GetTemporaryBlueprintName(NWN2ObjectType.Trigger, locationType), userOverrideDirectory, BWResourceTypes.GetResourceType("UTT"));
                    break;

                default:
                    throw new Exception("Unknown object type in CreateNewBlueprint()");
                }
            NWN2BlueprintCollection blueprintCollectionForType = instance.GetBlueprintCollectionForType(objectType);
            INWN2Object obj2 = cBlueprint as INWN2Object;
            obj2.Tag = cBlueprint.ResourceName.Value;
            obj2.LocalizedName[BWLanguages.CurrentLanguage] = cBlueprint.ResourceName.Value;
            cBlueprint.BlueprintLocation = instance.BlueprintLocation;
            blueprintCollectionForType.Add(cBlueprint);
            GFFFile file = new GFFFile();
            file.FileHeader.FileType = BWResourceTypes.GetFileExtension(cBlueprint.Resource.ResourceType);
            cBlueprint.SaveEverythingIntoGFFStruct(file.TopLevelStruct, true);
            using (Stream stream = cBlueprint.Resource.GetStream(true))
                {
                file.Save(stream);
                cBlueprint.Resource.Release();
                }
            return cBlueprint;
            }

        /// <summary>
        /// A utility class that is used to create the blueprint
        /// </summary>
        /// <param name="blueprint">The blueprints that will be created</param>
        /// <param name="text">The tempoary blueprint name</param>
        /// <param name="repository">The repository that the blueprint is to be saved in</param>
        /// <param name="num">The file type (I think)</param>
        private static void createHelp(INWN2Blueprint blueprint, string text, IResourceRepository repository, ushort num)
            {
            blueprint.Resource = repository.CreateResource(new OEIResRef(text), num);
            blueprint.TemplateResRef = new OEIResRef(blueprint.Resource.ResRef.Value);
            GFFFile file = new GFFFile();
            file.FileHeader.FileType = BWResourceTypes.GetFileExtension(num);
            blueprint.SaveEverythingIntoGFFStruct(file.TopLevelStruct, true);
            using (Stream stream = blueprint.Resource.GetStream(true))
                {
                file.Save(stream);
                blueprint.Resource.Release();
                }
            }

        /// <summary>
        /// If the form closes, then we need to make some clean up
        /// </summary>
        private void ModifyNew_FormClosing(object sender, FormClosingEventArgs e)
            {
            removeBlueprintActor();
            }

        /// <summary>
        /// Remove any non saved blueprints
        /// </summary>
        private void removeBlueprintActor()
            {
            if (!saveValue)
                {
                lastActor = null;
                if (propGrid.SelectedObjects != null && propGrid.SelectedObjects.Length > 0 && !edit)
                    {
                    NWN2Toolset.NWN2ToolsetMainForm.App.Module.DeleteBlueprint((INWN2Blueprint)propGrid.SelectedObjects[0]);
                    }
                }
            }
        }
    }
