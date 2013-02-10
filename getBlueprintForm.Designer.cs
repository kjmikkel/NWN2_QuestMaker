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

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuestMaker
{
    public partial class getBlueprint : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        this.select = new System.Windows.Forms.Button();
        this.cancel = new System.Windows.Forms.Button();
        this.dataBlueprints = new System.Windows.Forms.DataGridView();
        this.printName = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Blueprint = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.printTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.printArea = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.InstanceBlueprint = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.checkCreatures = new System.Windows.Forms.CheckBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.checkPlacables = new System.Windows.Forms.CheckBox();
        this.checkDoors = new System.Windows.Forms.CheckBox();
        this.checkItems = new System.Windows.Forms.CheckBox();
        this.checkBlueprints = new System.Windows.Forms.CheckBox();
        this.dataGridArea = new System.Windows.Forms.DataGridView();
        this.AreaTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Area = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Used = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        this.label3 = new System.Windows.Forms.Label();
        this.checkInstances = new System.Windows.Forms.CheckBox();
        this.checkTriggers = new System.Windows.Forms.CheckBox();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.checkModule = new System.Windows.Forms.CheckBox();
        this.checkCampagin = new System.Windows.Forms.CheckBox();
        this.label6 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.label8 = new System.Windows.Forms.Label();
        this.label9 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.dataBlueprints)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridArea)).BeginInit();
        this.SuspendLayout();
        // 
        // select
        // 
        this.select.Location = new System.Drawing.Point(713, 428);
        this.select.Name = "select";
        this.select.Size = new System.Drawing.Size(75, 23);
        this.select.TabIndex = 111;
        this.select.Text = "Select";
        this.select.UseVisualStyleBackColor = true;
        this.select.Click += new System.EventHandler(this.button1_Click);
        // 
        // cancel
        // 
        this.cancel.Location = new System.Drawing.Point(632, 428);
        this.cancel.Name = "cancel";
        this.cancel.Size = new System.Drawing.Size(75, 23);
        this.cancel.TabIndex = 10;
        this.cancel.Text = "Cancel";
        this.cancel.UseVisualStyleBackColor = true;
        this.cancel.Click += new System.EventHandler(this.button2_Click);
        // 
        // dataBlueprints
        // 
        this.dataBlueprints.AllowUserToAddRows = false;
        this.dataBlueprints.AllowUserToDeleteRows = false;
        this.dataBlueprints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataBlueprints.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.printName,
            this.Blueprint,
            this.printTag,
            this.printArea,
            this.InstanceBlueprint});
        this.dataBlueprints.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
        this.dataBlueprints.Location = new System.Drawing.Point(174, 39);
        this.dataBlueprints.Name = "dataBlueprints";
        this.dataBlueprints.ReadOnly = true;
        this.dataBlueprints.RowHeadersVisible = false;
        this.dataBlueprints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataBlueprints.Size = new System.Drawing.Size(404, 400);
        this.dataBlueprints.TabIndex = 4;
        this.dataBlueprints.TabStop = false;
        // 
        // printName
        // 
        this.printName.HeaderText = "Name";
        this.printName.Name = "printName";
        this.printName.ReadOnly = true;
        // 
        // Blueprint
        // 
        this.Blueprint.HeaderText = "Blueprint";
        this.Blueprint.Name = "Blueprint";
        this.Blueprint.ReadOnly = true;
        this.Blueprint.Visible = false;
        // 
        // printTag
        // 
        this.printTag.HeaderText = "Tag";
        this.printTag.Name = "printTag";
        this.printTag.ReadOnly = true;
        // 
        // printArea
        // 
        this.printArea.HeaderText = "Area";
        this.printArea.Name = "printArea";
        this.printArea.ReadOnly = true;
        // 
        // InstanceBlueprint
        // 
        this.InstanceBlueprint.HeaderText = "Instance/blueprint";
        this.InstanceBlueprint.Name = "InstanceBlueprint";
        this.InstanceBlueprint.ReadOnly = true;
        // 
        // checkCreatures
        // 
        this.checkCreatures.AutoSize = true;
        this.checkCreatures.Enabled = false;
        this.checkCreatures.Location = new System.Drawing.Point(592, 124);
        this.checkCreatures.Name = "checkCreatures";
        this.checkCreatures.Size = new System.Drawing.Size(103, 17);
        this.checkCreatures.TabIndex = 1;
        this.checkCreatures.Text = "Creatures/NPCs";
        this.checkCreatures.UseVisualStyleBackColor = true;
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(584, 39);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(122, 24);
        this.label1.TabIndex = 6;
        this.label1.Text = "Filter options:";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(585, 63);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(142, 26);
        this.label2.TabIndex = 7;
        this.label2.Text = "Please choose the filters you\r\n want enabled";
        // 
        // checkPlacables
        // 
        this.checkPlacables.AutoSize = true;
        this.checkPlacables.Enabled = false;
        this.checkPlacables.Location = new System.Drawing.Point(696, 124);
        this.checkPlacables.Name = "checkPlacables";
        this.checkPlacables.Size = new System.Drawing.Size(78, 17);
        this.checkPlacables.TabIndex = 2;
        this.checkPlacables.Text = "Placeables";
        this.checkPlacables.UseVisualStyleBackColor = true;
        // 
        // checkDoors
        // 
        this.checkDoors.AutoSize = true;
        this.checkDoors.Enabled = false;
        this.checkDoors.Location = new System.Drawing.Point(592, 147);
        this.checkDoors.Name = "checkDoors";
        this.checkDoors.Size = new System.Drawing.Size(54, 17);
        this.checkDoors.TabIndex = 3;
        this.checkDoors.Text = "Doors";
        this.checkDoors.UseVisualStyleBackColor = true;
        // 
        // checkItems
        // 
        this.checkItems.AutoSize = true;
        this.checkItems.Enabled = false;
        this.checkItems.Location = new System.Drawing.Point(592, 170);
        this.checkItems.Name = "checkItems";
        this.checkItems.Size = new System.Drawing.Size(51, 17);
        this.checkItems.TabIndex = 5;
        this.checkItems.Text = "Items";
        this.checkItems.UseVisualStyleBackColor = true;
        // 
        // checkBlueprints
        // 
        this.checkBlueprints.AutoSize = true;
        this.checkBlueprints.Checked = true;
        this.checkBlueprints.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkBlueprints.Location = new System.Drawing.Point(594, 282);
        this.checkBlueprints.Name = "checkBlueprints";
        this.checkBlueprints.Size = new System.Drawing.Size(72, 17);
        this.checkBlueprints.TabIndex = 7;
        this.checkBlueprints.Text = "Blueprints";
        this.checkBlueprints.UseVisualStyleBackColor = true;
        this.checkBlueprints.CheckedChanged += new System.EventHandler(this.checkBlueprints_CheckedChanged);
        // 
        // dataGridArea
        // 
        this.dataGridArea.AllowUserToAddRows = false;
        this.dataGridArea.AllowUserToDeleteRows = false;
        this.dataGridArea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridArea.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AreaTag,
            this.Area,
            this.Used});
        this.dataGridArea.Location = new System.Drawing.Point(12, 39);
        this.dataGridArea.Name = "dataGridArea";
        this.dataGridArea.ReadOnly = true;
        this.dataGridArea.RowHeadersVisible = false;
        this.dataGridArea.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridArea.Size = new System.Drawing.Size(156, 400);
        this.dataGridArea.TabIndex = 12;
        this.dataGridArea.TabStop = false;
        // 
        // AreaTag
        // 
        this.AreaTag.HeaderText = "Area tag";
        this.AreaTag.Name = "AreaTag";
        this.AreaTag.ReadOnly = true;
        this.AreaTag.Resizable = System.Windows.Forms.DataGridViewTriState.False;
        this.AreaTag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // Area
        // 
        this.Area.HeaderText = "Area";
        this.Area.Name = "Area";
        this.Area.ReadOnly = true;
        this.Area.Visible = false;
        // 
        // Used
        // 
        this.Used.HeaderText = "Used";
        this.Used.Name = "Used";
        this.Used.ReadOnly = true;
        this.Used.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.Used.Width = 40;
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(8, 9);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(64, 24);
        this.label3.TabIndex = 13;
        this.label3.Text = "Areas:";
        // 
        // checkInstances
        // 
        this.checkInstances.AutoSize = true;
        this.checkInstances.Checked = true;
        this.checkInstances.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkInstances.Location = new System.Drawing.Point(592, 236);
        this.checkInstances.Name = "checkInstances";
        this.checkInstances.Size = new System.Drawing.Size(72, 17);
        this.checkInstances.TabIndex = 6;
        this.checkInstances.Text = "Instances";
        this.checkInstances.UseVisualStyleBackColor = true;
        // 
        // checkTriggers
        // 
        this.checkTriggers.AutoSize = true;
        this.checkTriggers.Enabled = false;
        this.checkTriggers.Location = new System.Drawing.Point(696, 147);
        this.checkTriggers.Name = "checkTriggers";
        this.checkTriggers.Size = new System.Drawing.Size(64, 17);
        this.checkTriggers.TabIndex = 4;
        this.checkTriggers.Text = "Triggers";
        this.checkTriggers.UseVisualStyleBackColor = true;
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(588, 101);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(117, 20);
        this.label4.TabIndex = 16;
        this.label4.Text = "General Filters:";
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.Location = new System.Drawing.Point(588, 213);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(112, 20);
        this.label5.TabIndex = 17;
        this.label5.Text = "Special Filters:";
        // 
        // checkModule
        // 
        this.checkModule.AutoSize = true;
        this.checkModule.Checked = true;
        this.checkModule.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkModule.Location = new System.Drawing.Point(696, 333);
        this.checkModule.Name = "checkModule";
        this.checkModule.Size = new System.Drawing.Size(61, 17);
        this.checkModule.TabIndex = 9;
        this.checkModule.Text = "Module";
        this.checkModule.UseVisualStyleBackColor = true;
        // 
        // checkCampagin
        // 
        this.checkCampagin.AutoSize = true;
        this.checkCampagin.Checked = true;
        this.checkCampagin.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkCampagin.Location = new System.Drawing.Point(593, 333);
        this.checkCampagin.Name = "checkCampagin";
        this.checkCampagin.Size = new System.Drawing.Size(73, 17);
        this.checkCampagin.TabIndex = 8;
        this.checkCampagin.Text = "Campaign";
        this.checkCampagin.UseVisualStyleBackColor = true;
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(591, 302);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(192, 26);
        this.label6.TabIndex = 20;
        this.label6.Text = "Load blueprints - comes either from the \r\nmodule or the campaign\r\n";
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(589, 353);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(97, 39);
        this.label7.TabIndex = 21;
        this.label7.Text = "Load blueprints \r\nfrom the campaign \r\n(if there is one)";
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(693, 353);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(82, 26);
        this.label8.TabIndex = 22;
        this.label8.Text = "Load blueprints \r\nfrom the module";
        // 
        // label9
        // 
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(589, 256);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(211, 13);
        this.label9.TabIndex = 23;
        this.label9.Text = "Load instances - these will be listed by area";
        // 
        // getBlueprint
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 463);
        this.Controls.Add(this.label9);
        this.Controls.Add(this.label8);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.checkCampagin);
        this.Controls.Add(this.checkModule);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.checkTriggers);
        this.Controls.Add(this.checkInstances);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.dataGridArea);
        this.Controls.Add(this.checkBlueprints);
        this.Controls.Add(this.checkDoors);
        this.Controls.Add(this.checkItems);
        this.Controls.Add(this.checkPlacables);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.checkCreatures);
        this.Controls.Add(this.dataBlueprints);
        this.Controls.Add(this.cancel);
        this.Controls.Add(this.select);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "getBlueprint";
        this.Text = "Get Blueprint";
        ((System.ComponentModel.ISupportInitialize)(this.dataBlueprints)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataGridArea)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

        }

        #endregion

        private Button select;
        private Button cancel;
        private DataGridView dataBlueprints;
        private CheckBox checkCreatures;
        private Label label1;
        private Label label2;
        private CheckBox checkPlacables;
        private CheckBox checkDoors;
        private CheckBox checkItems;
        private CheckBox checkBlueprints;
        private DataGridView dataGridArea;
        private Label label3;
        private CheckBox checkInstances;
        private CheckBox checkTriggers;
        private Label label4;
        private Label label5;
        private DataGridViewTextBoxColumn printName;
        private DataGridViewTextBoxColumn Blueprint;
        private DataGridViewTextBoxColumn printTag;
        private DataGridViewTextBoxColumn printArea;
        private DataGridViewTextBoxColumn InstanceBlueprint;
        private DataGridViewTextBoxColumn AreaTag;
        private DataGridViewTextBoxColumn Area;
        private DataGridViewCheckBoxColumn Used;
        private CheckBox checkModule;
        private CheckBox checkCampagin;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;

    }
}
