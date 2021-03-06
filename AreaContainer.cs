﻿/* 
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
using System.Collections.Generic;
using System.Text;

using NWN2Toolset;
using NWN2Toolset.NWN2.Data;
using NWN2Toolset.NWN2.Data.Blueprints;
using NWN2Toolset.NWN2.Data.Instances;
using NWN2Toolset.NWN2.Data.Campaign;

namespace QuestMaker
    {
    class AreaContainer
        {
        public LinkedList<Actor> creaturePrints = new LinkedList<Actor>();
        public LinkedList<Actor> doorPrints = new LinkedList<Actor>();
        public LinkedList<Actor> placePrints = new LinkedList<Actor>();
        public LinkedList<Actor> triggerPrints = new LinkedList<Actor>();
        public LinkedList<Actor> itemPrints = new LinkedList<Actor>();
        public String tag;

        /// <summary>
        /// Constructor using an area
        /// </summary>
        /// <param name="area">The game area from where the actors will come from</param>
        public AreaContainer(NWN2GameArea area)
            {
            NWN2GameArea gameArea = area;
            gameArea.Demand();
            tag = gameArea.Tag;
            Actor act;

            foreach (NWN2CreatureInstance creature in gameArea.Creatures)
                {
                act = new Actor(creature, EnumTypes.actorType.Creature);
                creaturePrints.AddLast(act);
                }

            foreach (NWN2DoorInstance door in gameArea.Doors)
                {
                act = new Actor(door, EnumTypes.actorType.Door);
                doorPrints.AddLast(act);
                }

            foreach (NWN2PlaceableInstance place in gameArea.Placeables)
                {
                act = new Actor(place, EnumTypes.actorType.Placeable);
                placePrints.AddLast(act);
                }

            foreach (NWN2TriggerInstance trigger in gameArea.Triggers)
                {
                act = new Actor(trigger, EnumTypes.actorType.TriggerRegion);
                triggerPrints.AddLast(act);
                }

            foreach (NWN2ItemInstance item in gameArea.Items)
                {
                act = new Actor(item, EnumTypes.actorType.Item);
                itemPrints.AddLast(act);
                }
            }
         
        /// <summary>
        /// Creates the area container from the campaign currently being used
        /// </summary>
        public AreaContainer()
            {
            Actor act;
            tag = "Campagin";
            NWN2Campaign activeCampaign = NWN2CampaignManager.Instance.ActiveCampaign;
            if (activeCampaign == null)
                return;

            foreach (NWN2CreatureBlueprint creature in activeCampaign.Creatures)
                {
                act = new Actor(creature, EnumTypes.actorType.Creature);
                creaturePrints.AddLast(act);
                }

            foreach (NWN2DoorBlueprint door in activeCampaign.Doors)
                {
                act = new Actor(door, EnumTypes.actorType.Door);
                doorPrints.AddLast(act);
                }

            foreach (NWN2PlaceableBlueprint place in activeCampaign.Placeables)
                {
                act = new Actor(place, EnumTypes.actorType.Placeable);
                placePrints.AddLast(act);
                }

            foreach (NWN2TriggerBlueprint trigger in activeCampaign.Triggers)
                {
                act = new Actor(trigger, EnumTypes.actorType.TriggerRegion);
                triggerPrints.AddLast(act);
                }

            foreach (NWN2LightBlueprint item in activeCampaign.Items)
                {
                act = new Actor(item, EnumTypes.actorType.Item);
                itemPrints.AddLast(act);
                }
            }
        
        /// <summary>
        /// The constructor for the area continer - gets the actors from the module given
        /// </summary>
        /// <param name="module">The module from where I have to create the area list from</param>
        public AreaContainer(NWN2GameModule module)
        {
            Actor act;
            tag = "Module";
            if (module == null)
                return;

            foreach (NWN2CreatureBlueprint creature in module.Creatures)
                {
                act = new Actor(creature, EnumTypes.actorType.Creature);
                creaturePrints.AddLast(act);
                }

            foreach (NWN2DoorBlueprint door in module.Doors)
                {
                act = new Actor(door, EnumTypes.actorType.Door);
                doorPrints.AddLast(act);
                }

            foreach (NWN2PlaceableBlueprint place in module.Placeables)
                {
                act = new Actor(place, EnumTypes.actorType.Placeable);
                placePrints.AddLast(act);
                }

            foreach (NWN2TriggerBlueprint trigger in module.Triggers)
                {
                act = new Actor(trigger, EnumTypes.actorType.TriggerRegion);
                triggerPrints.AddLast(act);
                }

            foreach (NWN2ItemBlueprint item in module.Items)
                {
                act = new Actor(item, EnumTypes.actorType.Item);
                itemPrints.AddLast(act);
                }
            }
        
        }
    }
