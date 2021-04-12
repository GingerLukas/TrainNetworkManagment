
namespace TrainStation
{
    partial class SelectableDetail
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._lblName = new System.Windows.Forms.Label();
            this._lblType = new System.Windows.Forms.Label();
            this._lbMain = new System.Windows.Forms.ListBox();
            this._btnAction = new System.Windows.Forms.Button();
            this._lblState = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _lblName
            // 
            this._lblName.AutoSize = true;
            this._lblName.Location = new System.Drawing.Point(0, 0);
            this._lblName.Name = "_lblName";
            this._lblName.Size = new System.Drawing.Size(39, 15);
            this._lblName.TabIndex = 0;
            this._lblName.Text = "Name";
            // 
            // _lblType
            // 
            this._lblType.AutoSize = true;
            this._lblType.Location = new System.Drawing.Point(-1, 19);
            this._lblType.Name = "_lblType";
            this._lblType.Size = new System.Drawing.Size(31, 15);
            this._lblType.TabIndex = 1;
            this._lblType.Text = "Type";
            // 
            // _lbMain
            // 
            this._lbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._lbMain.FormattingEnabled = true;
            this._lbMain.ItemHeight = 15;
            this._lbMain.Location = new System.Drawing.Point(0, 84);
            this._lbMain.Name = "_lbMain";
            this._lbMain.Size = new System.Drawing.Size(286, 124);
            this._lbMain.TabIndex = 2;
            // 
            // _btnAction
            // 
            this._btnAction.Location = new System.Drawing.Point(0, 55);
            this._btnAction.Name = "_btnAction";
            this._btnAction.Size = new System.Drawing.Size(75, 23);
            this._btnAction.TabIndex = 3;
            this._btnAction.Text = "Set goal";
            this._btnAction.UseVisualStyleBackColor = true;
            // 
            // _lblState
            // 
            this._lblState.AutoSize = true;
            this._lblState.Location = new System.Drawing.Point(1, 37);
            this._lblState.Name = "_lblState";
            this._lblState.Size = new System.Drawing.Size(33, 15);
            this._lblState.TabIndex = 4;
            this._lblState.Text = "State";
            // 
            // SelectableDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._lblState);
            this.Controls.Add(this._btnAction);
            this.Controls.Add(this._lbMain);
            this.Controls.Add(this._lblType);
            this.Controls.Add(this._lblName);
            this.Name = "SelectableDetail";
            this.Size = new System.Drawing.Size(286, 208);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lblName;
        private System.Windows.Forms.Label _lblType;
        private System.Windows.Forms.ListBox _lbMain;
        private System.Windows.Forms.Button _btnAction;
        private System.Windows.Forms.Label _lblState;
    }
}
