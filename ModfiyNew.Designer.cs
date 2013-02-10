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

﻿namespace QuestMaker
    {
    partial class ModifyNew
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
            this.propGrid = new NWN2Toolset.NWN2.Views.NWN2PropertyGrid();
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.createBlueprint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // propGrid
            // 
            this.propGrid.Location = new System.Drawing.Point(0, 2);
            this.propGrid.Minimal = false;
            this.propGrid.Name = "propGrid";
            this.propGrid.ShowBlueprintChangeToolbarButtons = true;
            this.propGrid.ShowPreview = true;
            this.propGrid.Size = new System.Drawing.Size(327, 552);
            this.propGrid.TabIndex = 0;
            this.propGrid.UndoObject = null;
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(12, 609);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(241, 609);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // comboType
            // 
            this.comboType.FormattingEnabled = true;
            this.comboType.Location = new System.Drawing.Point(98, 583);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(121, 21);
            this.comboType.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 567);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Choose Type";
            // 
            // createBlueprint
            // 
            this.createBlueprint.Location = new System.Drawing.Point(98, 609);
            this.createBlueprint.Name = "createBlueprint";
            this.createBlueprint.Size = new System.Drawing.Size(126, 23);
            this.createBlueprint.TabIndex = 5;
            this.createBlueprint.Text = "Create new Blueprint";
            this.createBlueprint.UseVisualStyleBackColor = true;
            this.createBlueprint.Click += new System.EventHandler(this.button1_Click);
            // 
            // ModifyNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 648);
            this.Controls.Add(this.createBlueprint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboType);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.propGrid);
            this.MaximizeBox = false;
            this.Name = "ModifyNew";
            this.Text = "EditCreate";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ModifyNew_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

            }

        #endregion

        private NWN2Toolset.NWN2.Views.NWN2PropertyGrid propGrid;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button createBlueprint;
        }
    }
