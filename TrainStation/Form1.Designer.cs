
namespace TrainStation
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._renderTimer = new System.Windows.Forms.Timer(this.components);
            this._msMain = new System.Windows.Forms.MenuStrip();
            this._giNodes = new System.Windows.Forms.ToolStripMenuItem();
            this._miNodesBuild = new System.Windows.Forms.ToolStripMenuItem();
            this._miNodesConnect = new System.Windows.Forms.ToolStripMenuItem();
            this._giTrains = new System.Windows.Forms.ToolStripMenuItem();
            this._miTrainsNew = new System.Windows.Forms.ToolStripMenuItem();
            this._miTrainsRemoveAll = new System.Windows.Forms.ToolStripMenuItem();
            this._miSimulation = new System.Windows.Forms.ToolStripMenuItem();
            this._statusMain = new System.Windows.Forms.StatusStrip();
            this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._seletedDetail = new TrainStation.SelectableDetail();
            this._msMain.SuspendLayout();
            this._statusMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // _renderTimer
            // 
            this._renderTimer.Interval = 10;
            this._renderTimer.Tick += new System.EventHandler(this._renderTimer_Tick);
            // 
            // _msMain
            // 
            this._msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._giNodes,
            this._giTrains,
            this._miSimulation});
            this._msMain.Location = new System.Drawing.Point(0, 0);
            this._msMain.Name = "_msMain";
            this._msMain.Size = new System.Drawing.Size(1082, 24);
            this._msMain.TabIndex = 4;
            this._msMain.Text = "menuStrip1";
            // 
            // _giNodes
            // 
            this._giNodes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._miNodesBuild,
            this._miNodesConnect});
            this._giNodes.Name = "_giNodes";
            this._giNodes.Size = new System.Drawing.Size(53, 20);
            this._giNodes.Text = "&Nodes";
            // 
            // _miNodesBuild
            // 
            this._miNodesBuild.Name = "_miNodesBuild";
            this._miNodesBuild.Size = new System.Drawing.Size(119, 22);
            this._miNodesBuild.Text = "&Build";
            // 
            // _miNodesConnect
            // 
            this._miNodesConnect.Name = "_miNodesConnect";
            this._miNodesConnect.Size = new System.Drawing.Size(119, 22);
            this._miNodesConnect.Text = "&Connect";
            // 
            // _giTrains
            // 
            this._giTrains.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._miTrainsNew,
            this._miTrainsRemoveAll});
            this._giTrains.Name = "_giTrains";
            this._giTrains.Size = new System.Drawing.Size(49, 20);
            this._giTrains.Text = "&Trains";
            // 
            // _miTrainsNew
            // 
            this._miTrainsNew.Name = "_miTrainsNew";
            this._miTrainsNew.Size = new System.Drawing.Size(132, 22);
            this._miTrainsNew.Text = "&New";
            // 
            // _miTrainsRemoveAll
            // 
            this._miTrainsRemoveAll.Name = "_miTrainsRemoveAll";
            this._miTrainsRemoveAll.Size = new System.Drawing.Size(132, 22);
            this._miTrainsRemoveAll.Text = "&Remove all";
            // 
            // _miSimulation
            // 
            this._miSimulation.Name = "_miSimulation";
            this._miSimulation.Size = new System.Drawing.Size(76, 20);
            this._miSimulation.Text = "&Simulation";
            // 
            // _statusMain
            // 
            this._statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel});
            this._statusMain.Location = new System.Drawing.Point(0, 428);
            this._statusMain.Name = "_statusMain";
            this._statusMain.Size = new System.Drawing.Size(1082, 22);
            this._statusMain.TabIndex = 5;
            this._statusMain.Text = "statusStrip1";
            // 
            // _statusLabel
            // 
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(91, 17);
            this._statusLabel.Text = "Train simulation";
            // 
            // _seletedDetail
            // 
            this._seletedDetail.Item = null;
            this._seletedDetail.Location = new System.Drawing.Point(784, 198);
            this._seletedDetail.Name = "_seletedDetail";
            this._seletedDetail.Size = new System.Drawing.Size(286, 227);
            this._seletedDetail.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 450);
            this.Controls.Add(this._seletedDetail);
            this.Controls.Add(this._statusMain);
            this.Controls.Add(this._msMain);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this._msMain.ResumeLayout(false);
            this._msMain.PerformLayout();
            this._statusMain.ResumeLayout(false);
            this._statusMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer _renderTimer;
        private System.Windows.Forms.MenuStrip _msNodes;
        private System.Windows.Forms.MenuStrip _msMain;
        private System.Windows.Forms.ToolStripMenuItem _giNodes;
        private System.Windows.Forms.ToolStripMenuItem _miNodesBuild;
        private System.Windows.Forms.ToolStripMenuItem _giTrains;
        private System.Windows.Forms.ToolStripMenuItem _miTrainsNew;
        private System.Windows.Forms.ToolStripMenuItem _miTrainsRemoveAll;
        private System.Windows.Forms.ToolStripMenuItem _miNodesConnect;
        private System.Windows.Forms.StatusStrip _statusMain;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
        private System.Windows.Forms.ToolStripMenuItem _miSimulation;
        private SelectableDetail _seletedDetail;
    }
}

