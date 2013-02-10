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

namespace QuestMaker
{
	partial class QuestMain
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
        this.components = new System.ComponentModel.Container();
        this.questName = new System.Windows.Forms.TextBox();
        this.label1 = new System.Windows.Forms.Label();
        this.load = new System.Windows.Forms.Button();
        this.label5 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.MoveUpButton = new System.Windows.Forms.Button();
        this.moveDown = new System.Windows.Forms.Button();
        this.newsNodeButton = new System.Windows.Forms.Button();
        this.editsNodeButton = new System.Windows.Forms.Button();
        this.removesNodeButton = new System.Windows.Forms.Button();
        this.propRemove = new System.Windows.Forms.Button();
        this.propNew = new System.Windows.Forms.Button();
        this.propEdit = new System.Windows.Forms.Button();
        this.propBrowse = new System.Windows.Forms.Button();
        this.button21 = new System.Windows.Forms.Button();
        this.PropGrid = new System.Windows.Forms.DataGridView();
        this.propsQuestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.propsQuestTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.propsQuestImage = new System.Windows.Forms.DataGridViewImageColumn();
        this.propData = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.label8 = new System.Windows.Forms.Label();
        this.panel1 = new System.Windows.Forms.Panel();
        this.label14 = new System.Windows.Forms.Label();
        this.label13 = new System.Windows.Forms.Label();
        this.label12 = new System.Windows.Forms.Label();
        this.label11 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label10 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.villianRemove = new System.Windows.Forms.Button();
        this.label4 = new System.Windows.Forms.Label();
        this.GiverGrid = new System.Windows.Forms.DataGridView();
        this.giverQuestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.giverQuestTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.giverData = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.giverBrowse = new System.Windows.Forms.Button();
        this.ExtraGrid = new System.Windows.Forms.DataGridView();
        this.propName = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.progTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.extraData = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.giverEdit = new System.Windows.Forms.Button();
        this.VillianGrid = new System.Windows.Forms.DataGridView();
        this.villianQuestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.villianQuestTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.villianData = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.giverNew = new System.Windows.Forms.Button();
        this.extraRemove = new System.Windows.Forms.Button();
        this.villianBrowse = new System.Windows.Forms.Button();
        this.extraNew = new System.Windows.Forms.Button();
        this.villianEdit = new System.Windows.Forms.Button();
        this.extraEdit = new System.Windows.Forms.Button();
        this.villianNew = new System.Windows.Forms.Button();
        this.extraBrowse = new System.Windows.Forms.Button();
        this.label15 = new System.Windows.Forms.Label();
        this.label16 = new System.Windows.Forms.Label();
        this.sNodeList = new System.Windows.Forms.ListBox();
        this.label18 = new System.Windows.Forms.Label();
        this.label19 = new System.Windows.Forms.Label();
        this.triggerRegionsGrid = new System.Windows.Forms.DataGridView();
        this.triggerQuestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.triggerQuestTag = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.triggerData = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.triggerRemove = new System.Windows.Forms.Button();
        this.triggerNew = new System.Windows.Forms.Button();
        this.triggerEdit = new System.Windows.Forms.Button();
        this.triggerBrowse = new System.Windows.Forms.Button();
        this.comboPri = new System.Windows.Forms.ComboBox();
        this.comboLang = new System.Windows.Forms.ComboBox();
        this.label20 = new System.Windows.Forms.Label();
        this.label21 = new System.Windows.Forms.Label();
        this.genderBox = new System.Windows.Forms.ComboBox();
        this.label22 = new System.Windows.Forms.Label();
        this.save = new System.Windows.Forms.Button();
        this.QuestMainToolTip = new System.Windows.Forms.ToolTip(this.components);
        this.label7 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(this.PropGrid)).BeginInit();
        this.panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.GiverGrid)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.ExtraGrid)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.VillianGrid)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.triggerRegionsGrid)).BeginInit();
        this.SuspendLayout();
        // 
        // questName
        // 
        this.questName.Location = new System.Drawing.Point(12, 61);
        this.questName.Name = "questName";
        this.questName.Size = new System.Drawing.Size(258, 20);
        this.questName.TabIndex = 1;
        this.questName.Text = "test";
        // 
        // label1
        // 
        this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(8, 9);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(118, 20);
        this.label1.TabIndex = 16;
        this.label1.Text = "Quest Name:";
        // 
        // load
        // 
        this.load.Location = new System.Drawing.Point(7, 104);
        this.load.Name = "load";
        this.load.Size = new System.Drawing.Size(75, 23);
        this.load.TabIndex = 3;
        this.load.Text = "Load";
        this.QuestMainToolTip.SetToolTip(this.load, "Load selected Plot template (will clear all old inforamtion)");
        this.load.UseVisualStyleBackColor = true;
        this.load.Click += new System.EventHandler(this.load_Click);
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label5.Location = new System.Drawing.Point(401, 35);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(60, 20);
        this.label5.TabIndex = 20;
        this.label5.Text = "Props:";
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label6.Location = new System.Drawing.Point(401, 464);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(112, 20);
        this.label6.TabIndex = 21;
        this.label6.Text = "Story Nodes:";
        // 
        // MoveUpButton
        // 
        this.MoveUpButton.Enabled = false;
        this.MoveUpButton.Location = new System.Drawing.Point(648, 518);
        this.MoveUpButton.Name = "MoveUpButton";
        this.MoveUpButton.Size = new System.Drawing.Size(75, 23);
        this.MoveUpButton.TabIndex = 28;
        this.MoveUpButton.Text = "Move Up";
        this.MoveUpButton.UseVisualStyleBackColor = true;
        this.MoveUpButton.Click += new System.EventHandler(this.MoveUp);
        // 
        // moveDown
        // 
        this.moveDown.Enabled = false;
        this.moveDown.Location = new System.Drawing.Point(648, 550);
        this.moveDown.Name = "moveDown";
        this.moveDown.Size = new System.Drawing.Size(75, 23);
        this.moveDown.TabIndex = 29;
        this.moveDown.Text = "Move Down";
        this.moveDown.UseVisualStyleBackColor = true;
        this.moveDown.Click += new System.EventHandler(this.MoveDown);
        // 
        // newsNodeButton
        // 
        this.newsNodeButton.Enabled = false;
        this.newsNodeButton.Location = new System.Drawing.Point(405, 728);
        this.newsNodeButton.Name = "newsNodeButton";
        this.newsNodeButton.Size = new System.Drawing.Size(75, 23);
        this.newsNodeButton.TabIndex = 33;
        this.newsNodeButton.Text = "New";
        this.QuestMainToolTip.SetToolTip(this.newsNodeButton, "Create a new Story node");
        this.newsNodeButton.UseVisualStyleBackColor = true;
        this.newsNodeButton.Click += new System.EventHandler(this.newStoryNode);
        // 
        // editsNodeButton
        // 
        this.editsNodeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        this.editsNodeButton.Enabled = false;
        this.editsNodeButton.Location = new System.Drawing.Point(486, 728);
        this.editsNodeButton.Name = "editsNodeButton";
        this.editsNodeButton.Size = new System.Drawing.Size(75, 23);
        this.editsNodeButton.TabIndex = 34;
        this.editsNodeButton.Text = "Edit";
        this.QuestMainToolTip.SetToolTip(this.editsNodeButton, "Edit an existing story node");
        this.editsNodeButton.UseVisualStyleBackColor = true;
        this.editsNodeButton.Click += new System.EventHandler(this.editStoryNode);
        // 
        // removesNodeButton
        // 
        this.removesNodeButton.Enabled = false;
        this.removesNodeButton.Location = new System.Drawing.Point(567, 728);
        this.removesNodeButton.Name = "removesNodeButton";
        this.removesNodeButton.Size = new System.Drawing.Size(75, 23);
        this.removesNodeButton.TabIndex = 35;
        this.removesNodeButton.Text = "Remove";
        this.QuestMainToolTip.SetToolTip(this.removesNodeButton, "Remove an existing story node");
        this.removesNodeButton.UseVisualStyleBackColor = true;
        this.removesNodeButton.Click += new System.EventHandler(this.removesNodeButton_Click);
        // 
        // propRemove
        // 
        this.propRemove.Enabled = false;
        this.propRemove.Location = new System.Drawing.Point(648, 231);
        this.propRemove.Name = "propRemove";
        this.propRemove.Size = new System.Drawing.Size(75, 23);
        this.propRemove.TabIndex = 23;
        this.propRemove.Text = "Remove";
        this.QuestMainToolTip.SetToolTip(this.propRemove, "Remove an item");
        this.propRemove.UseVisualStyleBackColor = true;
        this.propRemove.Click += new System.EventHandler(this.propDelete_Click);
        // 
        // propNew
        // 
        this.propNew.Location = new System.Drawing.Point(405, 231);
        this.propNew.Name = "propNew";
        this.propNew.Size = new System.Drawing.Size(75, 23);
        this.propNew.TabIndex = 20;
        this.propNew.Text = "New";
        this.propNew.UseVisualStyleBackColor = true;
        this.propNew.Click += new System.EventHandler(this.propNew_Click);
        // 
        // propEdit
        // 
        this.propEdit.Enabled = false;
        this.propEdit.Location = new System.Drawing.Point(486, 231);
        this.propEdit.Name = "propEdit";
        this.propEdit.Size = new System.Drawing.Size(75, 23);
        this.propEdit.TabIndex = 21;
        this.propEdit.Text = "Edit";
        this.propEdit.UseVisualStyleBackColor = true;
        this.propEdit.Click += new System.EventHandler(this.propEdit_Click);
        // 
        // propBrowse
        // 
        this.propBrowse.Location = new System.Drawing.Point(567, 231);
        this.propBrowse.Name = "propBrowse";
        this.propBrowse.Size = new System.Drawing.Size(75, 23);
        this.propBrowse.TabIndex = 22;
        this.propBrowse.Text = "Browse";
        this.QuestMainToolTip.SetToolTip(this.propBrowse, "Select an item from either a blueprint or an instance");
        this.propBrowse.UseVisualStyleBackColor = true;
        this.propBrowse.Click += new System.EventHandler(this.propBrowse_Click);
        // 
        // button21
        // 
        this.button21.Location = new System.Drawing.Point(661, 727);
        this.button21.Name = "button21";
        this.button21.Size = new System.Drawing.Size(108, 23);
        this.button21.TabIndex = 36;
        this.button21.Text = "Create Entire Quest";
        this.QuestMainToolTip.SetToolTip(this.button21, "Generate values");
        this.button21.UseVisualStyleBackColor = true;
        this.button21.Click += new System.EventHandler(this.Done_Click);
        // 
        // PropGrid
        // 
        this.PropGrid.AllowUserToAddRows = false;
        this.PropGrid.AllowUserToDeleteRows = false;
        this.PropGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
        this.PropGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.PropGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.propsQuestName,
            this.propsQuestTag,
            this.propsQuestImage,
            this.propData});
        this.PropGrid.Location = new System.Drawing.Point(405, 95);
        this.PropGrid.Name = "PropGrid";
        this.PropGrid.ReadOnly = true;
        this.PropGrid.RowHeadersVisible = false;
        this.PropGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        this.PropGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
        this.PropGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.PropGrid.Size = new System.Drawing.Size(306, 130);
        this.PropGrid.TabIndex = 51;
        this.PropGrid.TabStop = false;
        this.PropGrid.SelectionChanged += new System.EventHandler(this.editRemoveStatus);
        // 
        // propsQuestName
        // 
        this.propsQuestName.HeaderText = "Name";
        this.propsQuestName.Name = "propsQuestName";
        this.propsQuestName.ReadOnly = true;
        // 
        // propsQuestTag
        // 
        this.propsQuestTag.HeaderText = "Tag";
        this.propsQuestTag.Name = "propsQuestTag";
        this.propsQuestTag.ReadOnly = true;
        // 
        // propsQuestImage
        // 
        this.propsQuestImage.HeaderText = "Image";
        this.propsQuestImage.Name = "propsQuestImage";
        this.propsQuestImage.ReadOnly = true;
        this.propsQuestImage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
        this.propsQuestImage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
        // 
        // propData
        // 
        this.propData.HeaderText = "propData";
        this.propData.Name = "propData";
        this.propData.ReadOnly = true;
        this.propData.Visible = false;
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(12, 35);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(264, 26);
        this.label8.TabIndex = 57;
        this.label8.Text = "Please enter the name  of the Quest. This will be used \r\nfor the journal entry";
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.label14);
        this.panel1.Controls.Add(this.label13);
        this.panel1.Controls.Add(this.label12);
        this.panel1.Controls.Add(this.label11);
        this.panel1.Controls.Add(this.label2);
        this.panel1.Controls.Add(this.label10);
        this.panel1.Controls.Add(this.label3);
        this.panel1.Controls.Add(this.villianRemove);
        this.panel1.Controls.Add(this.label4);
        this.panel1.Controls.Add(this.GiverGrid);
        this.panel1.Controls.Add(this.giverBrowse);
        this.panel1.Controls.Add(this.ExtraGrid);
        this.panel1.Controls.Add(this.giverEdit);
        this.panel1.Controls.Add(this.VillianGrid);
        this.panel1.Controls.Add(this.giverNew);
        this.panel1.Controls.Add(this.extraRemove);
        this.panel1.Controls.Add(this.villianBrowse);
        this.panel1.Controls.Add(this.extraNew);
        this.panel1.Controls.Add(this.villianEdit);
        this.panel1.Controls.Add(this.extraEdit);
        this.panel1.Controls.Add(this.villianNew);
        this.panel1.Controls.Add(this.extraBrowse);
        this.panel1.Location = new System.Drawing.Point(7, 156);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(331, 588);
        this.panel1.TabIndex = 6;
        // 
        // label14
        // 
        this.label14.AutoSize = true;
        this.label14.Location = new System.Drawing.Point(7, 423);
        this.label14.Name = "label14";
        this.label14.Size = new System.Drawing.Size(284, 13);
        this.label14.TabIndex = 82;
        this.label14.Text = "Please specify the extras in your quest - this field is optional";
        // 
        // label13
        // 
        this.label13.AutoSize = true;
        this.label13.Location = new System.Drawing.Point(7, 30);
        this.label13.Name = "label13";
        this.label13.Size = new System.Drawing.Size(317, 26);
        this.label13.TabIndex = 81;
        this.label13.Text = "The cast are the  that advance your quest. Cast members can be \r\nNPC\'s, doors or " +
            "placeables.";
        // 
        // label12
        // 
        this.label12.AutoSize = true;
        this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label12.Location = new System.Drawing.Point(6, 6);
        this.label12.Name = "label12";
        this.label12.Size = new System.Drawing.Size(51, 24);
        this.label12.TabIndex = 80;
        this.label12.Text = "Cast:";
        // 
        // label11
        // 
        this.label11.AutoSize = true;
        this.label11.Location = new System.Drawing.Point(14, 247);
        this.label11.Name = "label11";
        this.label11.Size = new System.Drawing.Size(219, 13);
        this.label11.TabIndex = 79;
        this.label11.Text = "Please specify the villain - this field is optional";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(5, 65);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(109, 20);
        this.label2.TabIndex = 61;
        this.label2.Text = "Quest Giver:";
        // 
        // label10
        // 
        this.label10.AutoSize = true;
        this.label10.Location = new System.Drawing.Point(14, 90);
        this.label10.Name = "label10";
        this.label10.Size = new System.Drawing.Size(289, 13);
        this.label10.TabIndex = 78;
        this.label10.Text = "Please specify the giver of the Quest - this field is mandatory";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label3.Location = new System.Drawing.Point(5, 218);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(62, 20);
        this.label3.TabIndex = 62;
        this.label3.Text = "Villain:";
        // 
        // villianRemove
        // 
        this.villianRemove.Enabled = false;
        this.villianRemove.Location = new System.Drawing.Point(252, 313);
        this.villianRemove.Name = "villianRemove";
        this.villianRemove.Size = new System.Drawing.Size(75, 23);
        this.villianRemove.TabIndex = 14;
        this.villianRemove.Text = "Remove";
        this.QuestMainToolTip.SetToolTip(this.villianRemove, "Remove the villian");
        this.villianRemove.UseVisualStyleBackColor = true;
        this.villianRemove.Click += new System.EventHandler(this.villianRemove_Click);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label4.Location = new System.Drawing.Point(5, 403);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(65, 20);
        this.label4.TabIndex = 63;
        this.label4.Text = "Extras:";
        // 
        // GiverGrid
        // 
        this.GiverGrid.AllowUserToAddRows = false;
        this.GiverGrid.AllowUserToDeleteRows = false;
        this.GiverGrid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
        this.GiverGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.GiverGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.giverQuestName,
            this.giverQuestTag,
            this.giverData});
        this.GiverGrid.Location = new System.Drawing.Point(7, 106);
        this.GiverGrid.Name = "GiverGrid";
        this.GiverGrid.ReadOnly = true;
        this.GiverGrid.RowHeadersVisible = false;
        this.GiverGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
        this.GiverGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.GiverGrid.Size = new System.Drawing.Size(203, 44);
        this.GiverGrid.TabIndex = 6;
        this.GiverGrid.TabStop = false;
        this.GiverGrid.SelectionChanged += new System.EventHandler(this.editRemoveStatus);
        // 
        // giverQuestName
        // 
        this.giverQuestName.HeaderText = "Name";
        this.giverQuestName.Name = "giverQuestName";
        this.giverQuestName.ReadOnly = true;
        // 
        // giverQuestTag
        // 
        this.giverQuestTag.HeaderText = "Tag";
        this.giverQuestTag.Name = "giverQuestTag";
        this.giverQuestTag.ReadOnly = true;
        // 
        // giverData
        // 
        this.giverData.HeaderText = "giverData";
        this.giverData.Name = "giverData";
        this.giverData.ReadOnly = true;
        this.giverData.Visible = false;
        // 
        // giverBrowse
        // 
        this.giverBrowse.Location = new System.Drawing.Point(171, 156);
        this.giverBrowse.Name = "giverBrowse";
        this.giverBrowse.Size = new System.Drawing.Size(75, 23);
        this.giverBrowse.TabIndex = 9;
        this.giverBrowse.Text = "Browse";
        this.QuestMainToolTip.SetToolTip(this.giverBrowse, "Select a Quest giver from either a blueprint or an instance");
        this.giverBrowse.UseVisualStyleBackColor = true;
        this.giverBrowse.Click += new System.EventHandler(this.giverBrowse_Click);
        // 
        // ExtraGrid
        // 
        this.ExtraGrid.AllowUserToAddRows = false;
        this.ExtraGrid.AllowUserToDeleteRows = false;
        this.ExtraGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
        this.ExtraGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.ExtraGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.propName,
            this.progTag,
            this.extraData});
        this.ExtraGrid.Location = new System.Drawing.Point(10, 439);
        this.ExtraGrid.Name = "ExtraGrid";
        this.ExtraGrid.ReadOnly = true;
        this.ExtraGrid.RowHeadersVisible = false;
        this.ExtraGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        this.ExtraGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
        this.ExtraGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.ExtraGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.ExtraGrid.Size = new System.Drawing.Size(200, 100);
        this.ExtraGrid.TabIndex = 15;
        this.ExtraGrid.TabStop = false;
        this.ExtraGrid.SelectionChanged += new System.EventHandler(this.editRemoveStatus);
        // 
        // propName
        // 
        this.propName.HeaderText = "Name";
        this.propName.Name = "propName";
        this.propName.ReadOnly = true;
        // 
        // progTag
        // 
        this.progTag.HeaderText = "Tag";
        this.progTag.Name = "progTag";
        this.progTag.ReadOnly = true;
        // 
        // extraData
        // 
        this.extraData.HeaderText = "extraData";
        this.extraData.Name = "extraData";
        this.extraData.ReadOnly = true;
        this.extraData.Visible = false;
        // 
        // giverEdit
        // 
        this.giverEdit.Enabled = false;
        this.giverEdit.Location = new System.Drawing.Point(90, 156);
        this.giverEdit.Name = "giverEdit";
        this.giverEdit.Size = new System.Drawing.Size(75, 23);
        this.giverEdit.TabIndex = 8;
        this.giverEdit.Text = "Edit";
        this.QuestMainToolTip.SetToolTip(this.giverEdit, "Edit currently selected Questgiver");
        this.giverEdit.UseVisualStyleBackColor = true;
        this.giverEdit.Click += new System.EventHandler(this.giverEdit_Click);
        // 
        // VillianGrid
        // 
        this.VillianGrid.AllowUserToAddRows = false;
        this.VillianGrid.AllowUserToDeleteRows = false;
        this.VillianGrid.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
        this.VillianGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.VillianGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.villianQuestName,
            this.villianQuestTag,
            this.villianData});
        this.VillianGrid.Location = new System.Drawing.Point(7, 263);
        this.VillianGrid.Name = "VillianGrid";
        this.VillianGrid.ReadOnly = true;
        this.VillianGrid.RowHeadersVisible = false;
        this.VillianGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
        this.VillianGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.VillianGrid.Size = new System.Drawing.Size(203, 44);
        this.VillianGrid.TabIndex = 10;
        this.VillianGrid.TabStop = false;
        this.VillianGrid.SelectionChanged += new System.EventHandler(this.editRemoveStatus);
        // 
        // villianQuestName
        // 
        this.villianQuestName.HeaderText = "Name";
        this.villianQuestName.Name = "villianQuestName";
        this.villianQuestName.ReadOnly = true;
        // 
        // villianQuestTag
        // 
        this.villianQuestTag.HeaderText = "Tag";
        this.villianQuestTag.Name = "villianQuestTag";
        this.villianQuestTag.ReadOnly = true;
        // 
        // villianData
        // 
        this.villianData.HeaderText = "villianData";
        this.villianData.Name = "villianData";
        this.villianData.ReadOnly = true;
        this.villianData.Visible = false;
        // 
        // giverNew
        // 
        this.giverNew.Location = new System.Drawing.Point(9, 156);
        this.giverNew.Name = "giverNew";
        this.giverNew.Size = new System.Drawing.Size(75, 23);
        this.giverNew.TabIndex = 7;
        this.giverNew.Text = "New";
        this.QuestMainToolTip.SetToolTip(this.giverNew, "Create a new Quest giver");
        this.giverNew.UseVisualStyleBackColor = true;
        this.giverNew.Click += new System.EventHandler(this.giverNew_Click);
        // 
        // extraRemove
        // 
        this.extraRemove.Enabled = false;
        this.extraRemove.Location = new System.Drawing.Point(252, 549);
        this.extraRemove.Name = "extraRemove";
        this.extraRemove.Size = new System.Drawing.Size(75, 23);
        this.extraRemove.TabIndex = 19;
        this.extraRemove.Text = "Remove";
        this.QuestMainToolTip.SetToolTip(this.extraRemove, "Remove an extra");
        this.extraRemove.UseVisualStyleBackColor = true;
        this.extraRemove.Click += new System.EventHandler(this.extraRemove_Click);
        // 
        // villianBrowse
        // 
        this.villianBrowse.Location = new System.Drawing.Point(171, 313);
        this.villianBrowse.Name = "villianBrowse";
        this.villianBrowse.Size = new System.Drawing.Size(75, 23);
        this.villianBrowse.TabIndex = 13;
        this.villianBrowse.Text = "Browse";
        this.QuestMainToolTip.SetToolTip(this.villianBrowse, "Select a villian from either a blueprint or an instance");
        this.villianBrowse.UseVisualStyleBackColor = true;
        this.villianBrowse.Click += new System.EventHandler(this.villianBrowse_Click);
        // 
        // extraNew
        // 
        this.extraNew.Location = new System.Drawing.Point(9, 549);
        this.extraNew.Name = "extraNew";
        this.extraNew.Size = new System.Drawing.Size(75, 23);
        this.extraNew.TabIndex = 16;
        this.extraNew.Text = "New";
        this.QuestMainToolTip.SetToolTip(this.extraNew, "Create a new Extra");
        this.extraNew.UseVisualStyleBackColor = true;
        this.extraNew.Click += new System.EventHandler(this.extraNew_Click);
        // 
        // villianEdit
        // 
        this.villianEdit.Enabled = false;
        this.villianEdit.Location = new System.Drawing.Point(90, 313);
        this.villianEdit.Name = "villianEdit";
        this.villianEdit.Size = new System.Drawing.Size(75, 23);
        this.villianEdit.TabIndex = 12;
        this.villianEdit.Text = "Edit";
        this.villianEdit.UseVisualStyleBackColor = true;
        this.villianEdit.Click += new System.EventHandler(this.villianEdit_Click);
        // 
        // extraEdit
        // 
        this.extraEdit.Enabled = false;
        this.extraEdit.Location = new System.Drawing.Point(90, 548);
        this.extraEdit.Name = "extraEdit";
        this.extraEdit.Size = new System.Drawing.Size(75, 23);
        this.extraEdit.TabIndex = 17;
        this.extraEdit.Text = "Edit";
        this.extraEdit.UseVisualStyleBackColor = true;
        this.extraEdit.Click += new System.EventHandler(this.extraEdit_Click);
        // 
        // villianNew
        // 
        this.villianNew.Location = new System.Drawing.Point(9, 313);
        this.villianNew.Name = "villianNew";
        this.villianNew.Size = new System.Drawing.Size(75, 23);
        this.villianNew.TabIndex = 11;
        this.villianNew.Text = "New";
        this.QuestMainToolTip.SetToolTip(this.villianNew, "Create a new Villain");
        this.villianNew.UseVisualStyleBackColor = true;
        this.villianNew.Click += new System.EventHandler(this.villianNew_Click);
        // 
        // extraBrowse
        // 
        this.extraBrowse.Location = new System.Drawing.Point(171, 548);
        this.extraBrowse.Name = "extraBrowse";
        this.extraBrowse.Size = new System.Drawing.Size(75, 23);
        this.extraBrowse.TabIndex = 18;
        this.extraBrowse.Text = "Browse";
        this.QuestMainToolTip.SetToolTip(this.extraBrowse, "Select an extra from either a blueprint or an instance");
        this.extraBrowse.UseVisualStyleBackColor = true;
        this.extraBrowse.Click += new System.EventHandler(this.extraBrowse_Click);
        // 
        // label15
        // 
        this.label15.AutoSize = true;
        this.label15.Location = new System.Drawing.Point(410, 484);
        this.label15.Name = "label15";
        this.label15.Size = new System.Drawing.Size(348, 26);
        this.label15.TabIndex = 60;
        this.label15.Text = "Story nodes are the episodes of your quest in chronological order. \r\nEach node is" +
            " a step in the plot, and involves one of the cast in the quest\r\n";
        // 
        // label16
        // 
        this.label16.AutoSize = true;
        this.label16.Location = new System.Drawing.Point(410, 61);
        this.label16.Name = "label16";
        this.label16.Size = new System.Drawing.Size(250, 26);
        this.label16.TabIndex = 61;
        this.label16.Text = "Here you can specify the items that are going to be \r\nused during your quest - th" +
            "is field is optional";
        // 
        // sNodeList
        // 
        this.sNodeList.FormattingEnabled = true;
        this.sNodeList.Location = new System.Drawing.Point(413, 518);
        this.sNodeList.Name = "sNodeList";
        this.sNodeList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
        this.sNodeList.Size = new System.Drawing.Size(229, 199);
        this.sNodeList.TabIndex = 62;
        this.sNodeList.TabStop = false;
        this.sNodeList.SelectedValueChanged += new System.EventHandler(this.sNodeList_SelectedValueChanged);
        // 
        // label18
        // 
        this.label18.AutoSize = true;
        this.label18.Location = new System.Drawing.Point(402, 293);
        this.label18.Name = "label18";
        this.label18.Size = new System.Drawing.Size(326, 13);
        this.label18.TabIndex = 89;
        this.label18.Text = "Please specify the Trigger regions in your quest - this field is optional";
        // 
        // label19
        // 
        this.label19.AutoSize = true;
        this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label19.Location = new System.Drawing.Point(400, 273);
        this.label19.Name = "label19";
        this.label19.Size = new System.Drawing.Size(136, 20);
        this.label19.TabIndex = 83;
        this.label19.Text = "Trigger Regions";
        // 
        // triggerRegionsGrid
        // 
        this.triggerRegionsGrid.AllowUserToAddRows = false;
        this.triggerRegionsGrid.AllowUserToDeleteRows = false;
        this.triggerRegionsGrid.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
        this.triggerRegionsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.triggerRegionsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.triggerQuestName,
            this.triggerQuestTag,
            this.triggerData});
        this.triggerRegionsGrid.Location = new System.Drawing.Point(405, 309);
        this.triggerRegionsGrid.Name = "triggerRegionsGrid";
        this.triggerRegionsGrid.ReadOnly = true;
        this.triggerRegionsGrid.RowHeadersVisible = false;
        this.triggerRegionsGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        this.triggerRegionsGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
        this.triggerRegionsGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.triggerRegionsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.triggerRegionsGrid.Size = new System.Drawing.Size(203, 100);
        this.triggerRegionsGrid.TabIndex = 88;
        this.triggerRegionsGrid.TabStop = false;
        this.triggerRegionsGrid.SelectionChanged += new System.EventHandler(this.editRemoveStatus);
        // 
        // triggerQuestName
        // 
        this.triggerQuestName.HeaderText = "Name";
        this.triggerQuestName.Name = "triggerQuestName";
        this.triggerQuestName.ReadOnly = true;
        // 
        // triggerQuestTag
        // 
        this.triggerQuestTag.HeaderText = "Tag";
        this.triggerQuestTag.Name = "triggerQuestTag";
        this.triggerQuestTag.ReadOnly = true;
        // 
        // triggerData
        // 
        this.triggerData.HeaderText = "triggerData";
        this.triggerData.Name = "triggerData";
        this.triggerData.ReadOnly = true;
        this.triggerData.Visible = false;
        // 
        // triggerRemove
        // 
        this.triggerRemove.Enabled = false;
        this.triggerRemove.Location = new System.Drawing.Point(647, 419);
        this.triggerRemove.Name = "triggerRemove";
        this.triggerRemove.Size = new System.Drawing.Size(75, 23);
        this.triggerRemove.TabIndex = 27;
        this.triggerRemove.Text = "Remove";
        this.QuestMainToolTip.SetToolTip(this.triggerRemove, "Remove a trigger");
        this.triggerRemove.UseVisualStyleBackColor = true;
        this.triggerRemove.Click += new System.EventHandler(this.button1_Click);
        // 
        // triggerNew
        // 
        this.triggerNew.Location = new System.Drawing.Point(404, 419);
        this.triggerNew.Name = "triggerNew";
        this.triggerNew.Size = new System.Drawing.Size(75, 23);
        this.triggerNew.TabIndex = 24;
        this.triggerNew.Text = "New";
        this.triggerNew.UseVisualStyleBackColor = true;
        this.triggerNew.Click += new System.EventHandler(this.button2_Click);
        // 
        // triggerEdit
        // 
        this.triggerEdit.Enabled = false;
        this.triggerEdit.Location = new System.Drawing.Point(485, 418);
        this.triggerEdit.Name = "triggerEdit";
        this.triggerEdit.Size = new System.Drawing.Size(75, 23);
        this.triggerEdit.TabIndex = 25;
        this.triggerEdit.Text = "Edit";
        this.triggerEdit.UseVisualStyleBackColor = true;
        this.triggerEdit.Click += new System.EventHandler(this.button4_Click);
        // 
        // triggerBrowse
        // 
        this.triggerBrowse.Location = new System.Drawing.Point(566, 418);
        this.triggerBrowse.Name = "triggerBrowse";
        this.triggerBrowse.Size = new System.Drawing.Size(75, 23);
        this.triggerBrowse.TabIndex = 26;
        this.triggerBrowse.Text = "Browse";
        this.QuestMainToolTip.SetToolTip(this.triggerBrowse, "Select a Trigger from either a blueprint or an instance");
        this.triggerBrowse.UseVisualStyleBackColor = true;
        this.triggerBrowse.Click += new System.EventHandler(this.getTriggerRegions);
        // 
        // comboPri
        // 
        this.comboPri.FormattingEnabled = true;
        this.comboPri.Items.AddRange(new object[] {
            "Highest",
            "High",
            "Medium",
            "Low",
            "Lowest"});
        this.comboPri.Location = new System.Drawing.Point(647, 604);
        this.comboPri.Name = "comboPri";
        this.comboPri.Size = new System.Drawing.Size(121, 21);
        this.comboPri.TabIndex = 30;
        // 
        // comboLang
        // 
        this.comboLang.FormattingEnabled = true;
        this.comboLang.Items.AddRange(new object[] {
            "English",
            "French",
            "German",
            "Italien",
            "Spanish",
            "Polish",
            "Russian",
            "Korean",
            "Chinese (Traditional)",
            "Chinese (Simplified)",
            "Japanese"});
        this.comboLang.Location = new System.Drawing.Point(647, 651);
        this.comboLang.Name = "comboLang";
        this.comboLang.Size = new System.Drawing.Size(121, 21);
        this.comboLang.TabIndex = 31;
        this.comboLang.Text = "English";
        this.comboLang.SelectedIndexChanged += new System.EventHandler(this.comboLang_SelectedIndexChanged);
        // 
        // label20
        // 
        this.label20.AutoSize = true;
        this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label20.Location = new System.Drawing.Point(645, 581);
        this.label20.Name = "label20";
        this.label20.Size = new System.Drawing.Size(107, 20);
        this.label20.TabIndex = 92;
        this.label20.Text = "Quest Priority:";
        // 
        // label21
        // 
        this.label21.AutoSize = true;
        this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label21.Location = new System.Drawing.Point(645, 628);
        this.label21.Name = "label21";
        this.label21.Size = new System.Drawing.Size(85, 20);
        this.label21.TabIndex = 93;
        this.label21.Text = "Language:";
        // 
        // genderBox
        // 
        this.genderBox.FormattingEnabled = true;
        this.genderBox.Items.AddRange(new object[] {
            "Male",
            "Female",
            "Both",
            "Either",
            "None"});
        this.genderBox.Location = new System.Drawing.Point(648, 696);
        this.genderBox.Name = "genderBox";
        this.genderBox.Size = new System.Drawing.Size(121, 21);
        this.genderBox.TabIndex = 32;
        this.genderBox.SelectedIndexChanged += new System.EventHandler(this.genderBox_SelectedIndexChanged);
        // 
        // label22
        // 
        this.label22.AutoSize = true;
        this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label22.Location = new System.Drawing.Point(644, 675);
        this.label22.Name = "label22";
        this.label22.Size = new System.Drawing.Size(67, 20);
        this.label22.TabIndex = 95;
        this.label22.Text = "Gender:";
        // 
        // save
        // 
        this.save.Location = new System.Drawing.Point(88, 104);
        this.save.Name = "save";
        this.save.Size = new System.Drawing.Size(75, 23);
        this.save.TabIndex = 4;
        this.save.Text = "Save";
        this.QuestMainToolTip.SetToolTip(this.save, "Save current plot template");
        this.save.UseVisualStyleBackColor = true;
        this.save.Click += new System.EventHandler(this.save_Click);
        // 
        // label7
        // 
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(9, 130);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(109, 13);
        this.label7.TabIndex = 96;
        this.label7.Text = "Load or save a Quest";
        // 
        // QuestMain
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(779, 756);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.save);
        this.Controls.Add(this.label22);
        this.Controls.Add(this.genderBox);
        this.Controls.Add(this.label21);
        this.Controls.Add(this.label20);
        this.Controls.Add(this.comboLang);
        this.Controls.Add(this.comboPri);
        this.Controls.Add(this.label18);
        this.Controls.Add(this.label19);
        this.Controls.Add(this.triggerRegionsGrid);
        this.Controls.Add(this.triggerRemove);
        this.Controls.Add(this.triggerNew);
        this.Controls.Add(this.triggerEdit);
        this.Controls.Add(this.triggerBrowse);
        this.Controls.Add(this.sNodeList);
        this.Controls.Add(this.label16);
        this.Controls.Add(this.label15);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.label8);
        this.Controls.Add(this.PropGrid);
        this.Controls.Add(this.button21);
        this.Controls.Add(this.propRemove);
        this.Controls.Add(this.propNew);
        this.Controls.Add(this.propEdit);
        this.Controls.Add(this.propBrowse);
        this.Controls.Add(this.removesNodeButton);
        this.Controls.Add(this.editsNodeButton);
        this.Controls.Add(this.newsNodeButton);
        this.Controls.Add(this.moveDown);
        this.Controls.Add(this.MoveUpButton);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.questName);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.load);
        this.MaximizeBox = false;
        this.Name = "QuestMain";
        this.Text = "Quest Maker";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QuestMain_FormClosing);
        ((System.ComponentModel.ISupportInitialize)(this.PropGrid)).EndInit();
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.GiverGrid)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.ExtraGrid)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.VillianGrid)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.triggerRegionsGrid)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

        }
		private System.Windows.Forms.Button load;
		private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox questName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button MoveUpButton;
        private System.Windows.Forms.Button moveDown;
        private System.Windows.Forms.Button newsNodeButton;
        private System.Windows.Forms.Button editsNodeButton;
        private System.Windows.Forms.Button removesNodeButton;
        private System.Windows.Forms.Button propRemove;
        private System.Windows.Forms.Button propNew;
        private System.Windows.Forms.Button propEdit;
        private System.Windows.Forms.Button propBrowse;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.DataGridView PropGrid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button villianRemove;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView GiverGrid;
        private System.Windows.Forms.Button giverBrowse;
        private System.Windows.Forms.DataGridView ExtraGrid;
        private System.Windows.Forms.Button giverEdit;
        private System.Windows.Forms.DataGridView VillianGrid;
        private System.Windows.Forms.Button giverNew;
        private System.Windows.Forms.Button extraRemove;
        private System.Windows.Forms.Button villianBrowse;
        private System.Windows.Forms.Button extraNew;
        private System.Windows.Forms.Button villianEdit;
        private System.Windows.Forms.Button extraEdit;
        private System.Windows.Forms.Button villianNew;
        private System.Windows.Forms.Button extraBrowse;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ListBox sNodeList;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DataGridView triggerRegionsGrid;
        private System.Windows.Forms.Button triggerRemove;
        private System.Windows.Forms.Button triggerNew;
        private System.Windows.Forms.Button triggerEdit;
        private System.Windows.Forms.Button triggerBrowse;
        private System.Windows.Forms.ComboBox comboPri;
        private System.Windows.Forms.ComboBox comboLang;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox genderBox;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.DataGridViewTextBoxColumn giverQuestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn giverQuestTag;
        private System.Windows.Forms.DataGridViewTextBoxColumn giverData;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerQuestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerQuestTag;
        private System.Windows.Forms.DataGridViewTextBoxColumn triggerData;
        private System.Windows.Forms.DataGridViewTextBoxColumn villianQuestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn villianQuestTag;
        private System.Windows.Forms.DataGridViewTextBoxColumn villianData;
        private System.Windows.Forms.DataGridViewTextBoxColumn propsQuestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn propsQuestTag;
        private System.Windows.Forms.DataGridViewImageColumn propsQuestImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn propData;
        private System.Windows.Forms.ToolTip QuestMainToolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn propName;
        private System.Windows.Forms.DataGridViewTextBoxColumn progTag;
        private System.Windows.Forms.DataGridViewTextBoxColumn extraData;
        private System.Windows.Forms.Label label7;
		
	}
}
