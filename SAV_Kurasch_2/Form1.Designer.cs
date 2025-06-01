using System.Windows.Forms.DataVisualization.Charting;

namespace SAV_Kurasch_2
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
            SuspendLayout();
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);

            this.chart1 = new Chart();
            ChartArea chartArea1 = new ChartArea();

            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);

            chart1.Location = new System.Drawing.Point(20, 20);
            chart1.Size = new System.Drawing.Size(740, 450);
            chart1.Name = "chart1";

            this.Controls.Add(chart1);
        }

        #endregion
        private Chart chart1;

    }
}
