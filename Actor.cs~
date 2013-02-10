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
using NWN2Toolset;
using NWN2Toolset.NWN2.Data;
using NWN2Toolset.NWN2.Data.Blueprints;
using NWN2Toolset.NWN2.Data.Instances;
using OEIShared.IO;
using System.Text.RegularExpressions;
using NWN2Toolset.NWN2.Data.Journal;
using NWN2Toolset.NWN2.Data.Templates;
using NWN2Toolset.NWN2.Data.ConversationData;
using OEIShared.Utils;
using OEIShared.IO.TwoDA;

namespace QuestMaker
    {
    [Serializable]
    public class Actor
        {
        public INWN2Blueprint blueprint;
        public INWN2Instance instance;
        public OEIShared.Utils.BWLanguages.BWLanguage lang;
        public EnumTypes.actorType type;
        public bool boolInstance;
        public IResourceEntry doorTalk;
        private Regex getName = new Regex("\"?([\\w\\s]+)(?:\",)?");

        #region Constructors

        /// <summary>
        /// Creates an actor from a blueprint
        /// </summary>
        /// <param name="actor">The blueprint that the actor will contain</param>
        /// <param name="type">The type of the blueprint</param>
        public Actor(NWN2Toolset.NWN2.Data.Blueprints.INWN2Blueprint actor, EnumTypes.actorType type)
            {
            blueprint = actor;
            this.type = type;
            boolInstance = false;
            }

        /// <summary>
        /// Creates an actor from an instance
        /// </summary>
        /// <param name="actor">The instance that the actor is created from</param>
        /// <param name="type">The type of the instance</param>
        public Actor(NWN2Toolset.NWN2.Data.Instances.INWN2Instance actor, EnumTypes.actorType type)
            {
            instance = actor;
            this.type = type;
            boolInstance = true;
            }
        #endregion

        /// <summary>
        /// Adds a inventory item to the actor
        /// </summary>
        /// <param name="item">The blueprint for the inventory item</param>
        public void addInv(NWN2BlueprintInventoryItem item)
            {
            switch (type)
                {
                case EnumTypes.actorType.Creature:
                    ((NWN2CreatureBlueprint)blueprint).Inventory.Add(item);
                    break;

                case EnumTypes.actorType.Placeable:
                    ((NWN2PlaceableBlueprint)blueprint).Inventory.Add(item);
                    break;

                default:
                    throw new Exception("You must place an inventory Item in either a creature/NPC or a placable");
                }
            }

        /// <summary>
        /// Adds a inventory item to the actor
        /// </summary>
        /// <param name="item">The instance for the inventory item</param>
        public void addInv(NWN2InstanceInventoryItem item)
            {
            switch (type)
                {
                case EnumTypes.actorType.Creature:
                    ((NWN2CreatureInstance)instance).Inventory.Add(item);
                    break;

                case EnumTypes.actorType.Placeable:
                    ((NWN2PlaceableInstance)instance).Inventory.Add(item);
                    break;

                default:
                    throw new Exception("You must place an inventory Item in either a creature/NPC or a placable");
                }
            }

        #region Properties
        /// <summary>
        /// The ToString method for the actors
        /// </summary>
        /// <returns>The name of the actor</returns>
        public override string ToString()
            {
            string name = String.Empty;
            switch (type)
                {
                case EnumTypes.actorType.Creature:
                    if (boolInstance)
                        {
                        name = ((NWN2CreatureInstance)instance).LocalizedName[lang];
                        }
                    else
                        {
                        name = ((NWN2CreatureBlueprint)blueprint).LocalizedName[lang];
                        }
                    break;

                case EnumTypes.actorType.Door:
                    if (boolInstance)
                        {
                        name = ((NWN2DoorInstance)instance).LocalizedName[lang];
                        }
                    else
                        {
                        name = ((NWN2DoorBlueprint)blueprint).LocalizedName[lang];
                        }
                    break;

                case EnumTypes.actorType.Placeable:
                    if (boolInstance)
                        {
                        name = ((NWN2PlaceableInstance)instance).LocalizedName[lang];
                        }
                    else
                        {
                        name = ((NWN2PlaceableBlueprint)blueprint).LocalizedName[lang];
                        }
                    break;

                case EnumTypes.actorType.TriggerRegion:
                    if (boolInstance)
                        {
                        name = ((NWN2TriggerInstance)instance).LocalizedName[lang];
                        }
                    else
                        {
                        name = ((NWN2TriggerBlueprint)blueprint).LocalizedName[lang];
                        }
                    break;

                case EnumTypes.actorType.Item:
                    if (boolInstance)
                        {
                        name = ((NWN2ItemInstance)instance).LocalizedName[lang];
                        }
                    else
                        {
                        name = ((NWN2ItemBlueprint)blueprint).LocalizedName[lang];
                        }
                    break;
                }
            return name;
            }

        public string Tag
            {
            get
                {
                string tag = String.Empty;
                switch (type)
                    {
                    case EnumTypes.actorType.Creature:
                        if (boolInstance)
                            {
                            tag = ((NWN2CreatureInstance)instance).Tag;
                            }
                        else
                            {
                            tag = ((NWN2CreatureBlueprint)blueprint).Tag;
                            }
                        break;
                    case EnumTypes.actorType.Door:
                        if (boolInstance)
                            {
                            tag = ((NWN2DoorInstance)instance).Tag;
                            }
                        else
                            {
                            tag = ((NWN2DoorBlueprint)blueprint).Tag;
                            }
                        break;
                    case EnumTypes.actorType.Placeable:
                        if (boolInstance)
                            {
                            tag = ((NWN2PlaceableInstance)instance).Tag;
                            }
                        else
                            {
                            tag = ((NWN2PlaceableBlueprint)blueprint).Tag;
                            }
                        break;

                    case EnumTypes.actorType.TriggerRegion:
                        if (boolInstance)
                            {
                            tag = ((NWN2TriggerInstance)instance).Tag;
                            }
                        else
                            {
                            tag = ((NWN2TriggerBlueprint)blueprint).Tag;
                            }
                        break;

                    case EnumTypes.actorType.Item:
                        if (boolInstance)
                            {
                            tag = ((NWN2ItemInstance)instance).Tag;
                            }
                        else
                            {
                            tag = ((NWN2ItemBlueprint)blueprint).Tag;
                            }
                        break;
                    }
                return tag;
                }
            }

        public NWN2ScriptVarTable Var
            {
            get
                {
                NWN2ScriptVarTable table = new NWN2ScriptVarTable();
                switch (type)
                    {
                    case EnumTypes.actorType.Creature:
                        if (boolInstance)
                            {
                            table = ((NWN2CreatureInstance)instance).Variables;
                            }
                        else
                            {
                            table = ((NWN2CreatureBlueprint)blueprint).Variables;
                            }
                        break;
                    case EnumTypes.actorType.Door:
                        if (boolInstance)
                            {
                            table = ((NWN2DoorInstance)instance).Variables;
                            }
                        else
                            {
                            table = ((NWN2DoorBlueprint)blueprint).Variables;
                            }
                        break;
                    case EnumTypes.actorType.Placeable:
                        if (boolInstance)
                            {
                            table = ((NWN2PlaceableInstance)instance).Variables;
                            }
                        else
                            {
                            table = ((NWN2PlaceableBlueprint)blueprint).Variables;
                            }
                        break;

                    case EnumTypes.actorType.TriggerRegion:
                        if (boolInstance)
                            {
                            table = ((NWN2TriggerInstance)instance).Variables;
                            }
                        else
                            {
                            table = ((NWN2TriggerBlueprint)blueprint).Variables;
                            }
                        break;

                    case EnumTypes.actorType.Item:
                        if (boolInstance)
                            {
                            table = ((NWN2ItemInstance)instance).Variables;
                            }
                        else
                            {
                            table = ((NWN2ItemBlueprint)blueprint).Variables;
                            }
                        break;
                    }
                return table;
                }
            set
                {
                switch (type)
                    {
                    case EnumTypes.actorType.Creature:
                        if (boolInstance)
                            {
                            ((NWN2CreatureInstance)instance).Variables = value;
                            }
                        else
                            {
                            ((NWN2CreatureBlueprint)blueprint).Variables = value;
                            }
                        break;
                    case EnumTypes.actorType.Door:
                        if (boolInstance)
                            {
                            ((NWN2DoorInstance)instance).Variables = value;
                            }
                        else
                            {
                            ((NWN2DoorBlueprint)blueprint).Variables = value;
                            }
                        break;

                    case EnumTypes.actorType.Placeable:
                        if (boolInstance)
                            {
                            ((NWN2PlaceableInstance)instance).Variables = value;
                            }
                        else
                            {
                            ((NWN2PlaceableBlueprint)blueprint).Variables = value;
                            }
                        break;
                    }
                }
            }


        public IResourceEntry Conversation
            {
            get
                {
                IResourceEntry conv = null;

                switch (type)
                    {
                    case EnumTypes.actorType.Creature:
                        if (boolInstance)
                            {
                            conv = ((NWN2CreatureInstance)instance).Conversation;
                            }
                        else
                            {
                            conv = ((NWN2CreatureBlueprint)blueprint).Conversation;
                            }
                        break;

                    case EnumTypes.actorType.Door:
                        if (boolInstance)
                            {
                            conv = ((NWN2DoorInstance)instance).Conversation;
                            }
                        else
                            {
                            conv = ((NWN2DoorBlueprint)blueprint).Conversation;
                            }
                        break;

                    case EnumTypes.actorType.Placeable:
                        if (boolInstance)
                            {
                            conv = ((NWN2PlaceableInstance)instance).Conversation;
                            }
                        else
                            {
                            conv = ((NWN2PlaceableBlueprint)blueprint).Conversation;
                            }
                        break;
                    }
                return conv;
                }
            set
                {
                switch (type)
                    {
                    case EnumTypes.actorType.Creature:
                        if (boolInstance)
                            {
                            ((NWN2CreatureInstance)instance).Conversation = value;
                            }
                        else
                            {
                            ((NWN2CreatureBlueprint)blueprint).Conversation = value;
                            }
                        break;
                    case EnumTypes.actorType.Door:
                        if (boolInstance)
                            {
                            var instDoor = (NWN2DoorInstance)instance;
                            instDoor.OnFailToOpen = doorTalk;
                            // In order to active the above the door needs to be locked
                            instDoor.Locked = true;
                            instDoor.Conversation = value;
                            }
                        else
                            {
                            var blueDoor = (NWN2DoorBlueprint)blueprint;
                            blueDoor.OnFailToOpen = doorTalk;
                            blueDoor.Locked = true;
                            blueDoor.Resource = value;
                            }
                        break;

                    case EnumTypes.actorType.Placeable:
                        if (boolInstance)
                            {
                        var instPlaceable = (NWN2PlaceableInstance)instance;
                        instPlaceable.OnUsed = doorTalk;
                        // It cannot be static
                        instPlaceable.Static = false;
                        // The placeable has to be usable
                        instPlaceable.Usable = true;
                        // A placable you can talk to cannot have an inventory
                        instPlaceable.HasInventory = false;
                        instPlaceable.Conversation = value;
                            }
                        else
                            {
                        var bluePlaceable = (NWN2PlaceableBlueprint)blueprint;
                        bluePlaceable.OnUsed = doorTalk;
                        bluePlaceable.Static = false;
                        bluePlaceable.HasInventory = false;
                        bluePlaceable.Conversation = Resource;
                            }
                        break;
                    }
                }
            }

        public IResourceEntry Resource
            {
            get
                {
                switch (type)
                    {
                    case EnumTypes.actorType.Item:
                        if (!boolInstance)
                            {
                            return ((NWN2ItemBlueprint)blueprint).Resource;
                            }
                        else
                            {
                            throw new Exception("The item is not made from a blueprint");
                            }

                    default:
                        throw new Exception("The requested item does eithet not exist or is not a blueprint");
                    }
                }
            }

        public TwoDAReference Icon
            {
            get
                {
                if (boolInstance)
                    {
                    return ((NWN2ItemInstance)instance).Icon;
                    }
                else
                    {
                    return ((NWN2ItemBlueprint)blueprint).Icon;
                    }
                }
            }

        public TwoDAReference BaseItem
            {
            get
                {
                if (boolInstance)
                    {
                    return ((NWN2ItemInstance)instance).BaseItem;
                    }
                else
                    {
                    return ((NWN2ItemBlueprint)blueprint).BaseItem;
                    }
                }
            }

        /// <summary>
        /// Copies the actor
        /// </summary>
        /// <param name="actor"> The actor which is to be copied</param>
        /// <returns>The actor that is copied</returns>
        public static Actor Copy(Actor actor)
            {
            return (Actor)actor.MemberwiseClone();
            }
        #endregion
        }
    }
