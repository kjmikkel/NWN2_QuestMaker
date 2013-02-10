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

using System.Windows.Forms;
using NWN2Toolset.Plugins;
using NWN2Toolset;
using NWN2Toolset.NWN2.Data;
using NWN2Toolset.NWN2.Data.Journal;
using OEIShared.Utils;
using System.Reflection;
using TD.SandBar;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;

namespace QuestMaker
{
    public class QuestMaker : INWN2Plugin
        {

        #region Fields
        private MenuButtonItem m_cMenuItem;

        private String filePath;
        private const string name = "Quest Maker Plug-in";
        #endregion

        #region Essential
        private void HandlePluginLaunch(object sender, EventArgs e)
        {
            Form questMain = new QuestMain(ref filePath);
            questMain.Show();
            report("filePath: " + filePath);
        }

        public void Load(INWN2PluginHost cHost)
        {
        }

        public void Unload(INWN2PluginHost cHost)
            {
            }

        public void Startup(INWN2PluginHost cHost)
        {
            this.m_cMenuItem = cHost.GetMenuForPlugin(this);
            this.m_cMenuItem.Activate += new EventHandler(this.HandlePluginLaunch);
        }

        public void Shutdown(INWN2PluginHost cHost)
            {
            }

        // Properties
        public string DisplayName
        {
            get
            {
                return name;
            }
        }

        public string MenuName
        {
            get
            {
                return name;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public MenuButtonItem PluginMenuItem
        {
            get
            {
                return this.m_cMenuItem;
            }
        }

        #region Preferences
        
        public object Preferences
            {
            get
                {
                return null;
                }
            set { }
           /* 
            get
                {
                QuestPreferences prefs = new QuestPreferences();
                report("Into the pref" + filePath);
                prefs.questLocation = filePath;
                return prefs;
                }
            set
                {
                report("Setting value");
                QuestPreferences prefs = (QuestPreferences)value;
                report("Into the app: " + prefs.questLocation);
                filePath = prefs.questLocation;
                }
            */
            }
        #endregion
#endregion

        #region Not essential
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

        private static void report(String str)
            {
            System.Console.WriteLine(str);
            }
        #endregion
    }

    [Serializable]
    public class QuestPreferences
        {
        string filePath;

        [Category("Stored Quests"), DisplayName("Properties"), Browsable(true), Description("Location of the stored Quests")]
        public string questLocation
            {
            get { return filePath; }
            set { filePath = value; }
            }
        }
}
