
namespace ManagerForm;

partial class FormMain
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
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Text = "DNPCS Manager";

        this.labelSpeed = new System.Windows.Forms.Label();
        this.trackBarSpeed = new System.Windows.Forms.TrackBar();
        this.listBoxMonitor = new System.Windows.Forms.ListBox();
        this.statusLabel = new System.Windows.Forms.Label();
        this.buttonStartStop = new System.Windows.Forms.Button();

        this.SuspendLayout();

        //
        // labelSpeed
        //
        this.labelSpeed.AutoSize = false;
        this.labelSpeed.Location = new System.Drawing.Point(22, 12);
        this.labelSpeed.Name = "labelSpeed";
        this.labelSpeed.Size = new System.Drawing.Size(51, 20);
        this.labelSpeed.TabIndex = 1;
        this.labelSpeed.Text = "Speed";

        // 
        // trackBarSpeed
        // 
        this.trackBarSpeed.Location = new System.Drawing.Point(12, 39);
        this.trackBarSpeed.Name = "trackBarSpeed";
        this.trackBarSpeed.Size = new System.Drawing.Size(776, 56);
        this.trackBarSpeed.TabIndex = 3;
        
        // 
        // listBoxMonitor
        //
        this.listBoxMonitor.FormattingEnabled = true;
        this.listBoxMonitor.Location = new System.Drawing.Point(12, 310);
        this.listBoxMonitor.Name = "listBoxMonitor";
        this.listBoxMonitor.Size = new System.Drawing.Size(776, 104);
        this.listBoxMonitor.TabIndex = 4;

        // 
        // statusLabel
        // 
        this.statusLabel.AutoSize = true;
        this.statusLabel.Location = new System.Drawing.Point(4, 426);
        this.statusLabel.Name = "statusLabel";
        this.statusLabel.Size = new System.Drawing.Size(82, 20);
        this.statusLabel.TabIndex = 0;
        this.statusLabel.Text = "Initializing...";

        // 
        // buttonStartStop
        // 
        this.buttonStartStop.Location = new System.Drawing.Point(694, 417);
        this.buttonStartStop.Name = "buttonStartStop";
        this.buttonStartStop.Size = new System.Drawing.Size(94, 29);
        this.buttonStartStop.TabIndex = 5;
        this.buttonStartStop.Text = "Start";
        this.buttonStartStop.Enabled = true;

        this.buttonStartStop.Click += new System.EventHandler(this.ButtonStartStop_Click);

        // 
        // FormMain
        // 

        Controls.Add(labelSpeed);
        Controls.Add(trackBarSpeed);
        Controls.Add((listBoxMonitor));
        Controls.Add(statusLabel);
        Controls.Add(buttonStartStop);

    }


    private Label labelSpeed;
    private TrackBar trackBarSpeed;
    private ListBox listBoxMonitor;
    private Label statusLabel;
    private Button buttonStartStop;


    #endregion
}
