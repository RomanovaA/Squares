using System;
namespace Squares
{
    partial class GameForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {

            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch(Exception e)
            {
                //=(
            }
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel = new System.Windows.Forms.Panel();
            this.startPauseBtn = new System.Windows.Forms.Button();
            this.pointsLbl = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.startPauseBtn);
            this.panel.Location = new System.Drawing.Point(379, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(100, 520);
            this.panel.TabIndex = 0;
            // 
            // startPauseBtn
            // 
            this.startPauseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startPauseBtn.ForeColor = System.Drawing.Color.White;
            this.startPauseBtn.Image = global::Squares.Properties.Resources.btn_on;
            this.startPauseBtn.Location = new System.Drawing.Point(10, 11);
            this.startPauseBtn.Name = "startPauseBtn";
            this.startPauseBtn.Size = new System.Drawing.Size(75, 75);
            this.startPauseBtn.TabIndex = 0;
            this.startPauseBtn.UseVisualStyleBackColor = true;
            this.startPauseBtn.EnabledChanged += new System.EventHandler(this.startPauseBtn_EnabledChanged);
            this.startPauseBtn.Click += new System.EventHandler(this.startPauseBtn_Click);
            this.startPauseBtn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.startPauseBtn_KeyPress);
            this.startPauseBtn.MouseEnter += new System.EventHandler(this.startPauseBtn_MouseEnter);
            this.startPauseBtn.MouseLeave += new System.EventHandler(this.startPauseBtn_MouseLeave);
            // 
            // pointsLbl
            // 
            this.pointsLbl.BackColor = System.Drawing.SystemColors.Control;
            this.pointsLbl.Font = new System.Drawing.Font("Tempus Sans ITC", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointsLbl.ForeColor = System.Drawing.Color.DarkRed;
            this.pointsLbl.Location = new System.Drawing.Point(89, 474);
            this.pointsLbl.Name = "pointsLbl";
            this.pointsLbl.Size = new System.Drawing.Size(200, 50);
            this.pointsLbl.TabIndex = 1;
            this.pointsLbl.Text = "0";
            this.pointsLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 524);
            this.Controls.Add(this.pointsLbl);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Colored Squares";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button startPauseBtn;
        private System.Windows.Forms.Label pointsLbl;
    }
}

