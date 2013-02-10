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
using System.Collections.Generic;
using System.Text;

namespace QuestMaker
    {
    public static class EnumTypes
        {

        public enum actorType
            {
            Creature        = 1,
            Door            = 2,
            Placeable       = 4,
            TriggerRegion   = 8,
            Item            = 16,
            Actor           = 32,
            None            = 0
            }

        public enum happens
            {
            Conv        = 1,
            Conflict    = 2,
            OpenDoor    = 4,
            Trigger     = 8,
            None        = 0
            }

        public enum conv
            {
            QuestInfo   = 1,
            Single      = 2,
            None        = 0
            }

        public enum prereq
            {
            NoPrereq            = 0,
            SimplePrereq        = 1,
            CastSpecificPrereq  = 2
            }
        }
    }
