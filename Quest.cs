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
    [Serializable]
    public class Quest
        {

        public String name = "";

        public StoryNode[] storyNodes = new StoryNode[1];

        public Actor giver = null;
        public Actor villan = null;

        public Actor[] extras = new Actor[1];
        public Actor[] props = new Actor[1];
        public Actor[] triggers = new Actor[1];

        public override string ToString()
            {
            return name;
            }
        }
    }
