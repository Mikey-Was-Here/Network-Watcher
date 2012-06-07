using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;

using IpHlpApidotnet;

namespace NetWatch
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader remote;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button button4;

		private IpHlpApidotnet.IPHelper MyAPI;

		private const int MIB_TCP_RTO_CONSTANT=2;
		private const int MIB_TCP_RTO_OTHER=1;
		private const int MIB_TCP_RTO_RSRE=3;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.ColumnHeader ProcessName;
		private System.Windows.Forms.Button button7;	
		private const int MIB_TCP_RTO_VANJ=4;



		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			MyAPI = new IpHlpApidotnet.IPHelper();
		
							 
		}

		
	     #region Windows Form Designer generated code
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.remote = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.ProcessName = new System.Windows.Forms.ColumnHeader();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 32);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "Stat Tcp";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(8, 72);
			this.button2.Name = "button2";
			this.button2.TabIndex = 2;
			this.button2.Text = "Tcp Table";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(64, 152);
			this.button3.Name = "button3";
			this.button3.TabIndex = 3;
			this.button3.Text = "clear";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.remote,
																						this.columnHeader2,
																						this.columnHeader3,
																						this.ProcessName});
			this.listView1.FullRowSelect = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(224, 24);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(696, 400);
			this.listView1.TabIndex = 4;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Local";
			this.columnHeader1.Width = 200;
			// 
			// remote
			// 
			this.remote.Text = "remote";
			this.remote.Width = 200;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "State";
			this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader2.Width = 100;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "ProcessID";
			// 
			// ProcessName
			// 
			this.ProcessName.Text = "ProcessName";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(120, 72);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(72, 24);
			this.button4.TabIndex = 5;
			this.button4.Text = "udp table";
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(8, 112);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(88, 23);
			this.button5.TabIndex = 6;
			this.button5.Text = "Tcp Table XP";
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(272, 448);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(256, 16);
			this.label1.TabIndex = 7;
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(120, 112);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(88, 23);
			this.button6.TabIndex = 8;
			this.button6.Text = "UDP Table XP";
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(120, 32);
			this.button7.Name = "button7";
			this.button7.TabIndex = 9;
			this.button7.Text = "Udp Stat";
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(936, 526);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.button7,
																		  this.button6,
																		  this.label1,
																		  this.button5,
																		  this.button4,
																		  this.listView1,
																		  this.button3,
																		  this.button2,
																		  this.button1});
			this.Name = "Form1";
			this.Text = "IPHelperAPI";
			this.ResumeLayout(false);

		}
		

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			MyAPI.GetTcpStats();
			string m_algo="";
			switch(MyAPI.TcpStats.dwRtoAlgorithm)
			{
				case 1 : m_algo="Other";break;
				case 2 : m_algo="Constant Time-out";break;
				case 3 : m_algo="MIL-STD-1778 Appendix B";break;
				case 4 : m_algo="Van Jacobson's Algorithm";break;
			}
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "RtoAlgorithm",
																	   m_algo,
																	        ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "RtoMin",
																	   MyAPI.TcpStats.dwRtoMin.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "RtoMax",
																	   MyAPI.TcpStats.dwRtoMax.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "MAx Connexion",
																	   MyAPI.TcpStats.dwMaxConn.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Active Open Connexion",
																	   MyAPI.TcpStats.dwActiveOpens.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Passive Open Connexion",
																	   MyAPI.TcpStats.dwPassiveOpens.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Attempte Fail",
																	   MyAPI.TcpStats.dwAttemptFails.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Estabished connexion that have been reset",
																	   MyAPI.TcpStats.dwEstabResets.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Current estabished connexion",
																	   MyAPI.TcpStats.dwCurrEstab.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "In Segments",
																	   MyAPI.TcpStats.dwInSegs.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Out Segement",
																	   MyAPI.TcpStats.dwOutSegs.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Segement Retransmitted",
																	   MyAPI.TcpStats.dwRetransSegs.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "InErrors",
																	   MyAPI.TcpStats.dwInErrs.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "number of segments transmitted with the reset flag set",
																	   MyAPI.TcpStats.dwRetransSegs.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "number of connexions",
																	   MyAPI.TcpStats.dwNumConns.ToString(),
																	   ""
																   }));
		}


		private void button2_Click(object sender, System.EventArgs e)
		{
			
			MyAPI.GetTcpConnexions();
			for(int i=0;i<MyAPI.TcpConnexion.dwNumEntries;i++)
			{
				this.listView1.Items.Add(new ListViewItem(new string[] {
														MyAPI.TcpConnexion.table[i].Local.Address.ToString()+":"+MyAPI.TcpConnexion.table[i].Local.Port.ToString(),
														MyAPI.TcpConnexion.table[i].Remote.Address.ToString()+":"+MyAPI.TcpConnexion.table[i].Remote.Port.ToString(),
														MyAPI.TcpConnexion.table[i].StrgState.ToString()
								}));
			}
			this.label1.Text="NB connexion = "+MyAPI.TcpConnexion.dwNumEntries.ToString();
			

		}


		private void button3_Click(object sender, System.EventArgs e)
		{
			this.listView1.Items.Clear();
		}


		private void button4_Click(object sender, System.EventArgs e)
		{
			MyAPI.GetUdpConnexions();
			for(int i=0;i<MyAPI.UdpConnexion.dwNumEntries;i++)
			{
				this.listView1.Items.Add(new ListViewItem(new string[] {
																		   MyAPI.UdpConnexion.table[i].Local.Address.ToString()+":"+MyAPI.UdpConnexion.table[i].Local.Port.ToString(),
																		        "",""
																	   }));
			}
		}


		private void button5_Click(object sender, System.EventArgs e)
		{
			MyAPI.GetExTcpConnexions();
			for(int i=0;i<MyAPI.TcpExConnexions.dwNumEntries;i++)
				{
					this.listView1.Items.Add(new ListViewItem(new string[] {

							MyAPI.TcpExConnexions.table[i].Local.Address.ToString()+":"+MyAPI.TcpExConnexions.table[i].Local.Port.ToString(),
							MyAPI.TcpExConnexions.table[i].Remote.Address.ToString()+":"+MyAPI.TcpExConnexions.table[i].Remote.Port.ToString(),
							MyAPI.TcpExConnexions.table[i].StrgState.ToString(),
							MyAPI.TcpExConnexions.table[i].dwProcessId.ToString(),
							MyAPI.TcpExConnexions.table[i].ProcessName

						}));
				}
			this.label1.Text="NB connexion = "+MyAPI.TcpExConnexions.dwNumEntries.ToString();
		}


		private void button6_Click(object sender, System.EventArgs e)
		{
			MyAPI.GetExUdpConnexions();
			for(int i=0;i<MyAPI.UdpExConnexion.dwNumEntries;i++)
			{
				this.listView1.Items.Add(new ListViewItem(new string[] {

																		   MyAPI.UdpExConnexion.table[i].Local.Address.ToString()+":"+MyAPI.UdpExConnexion.table[i].Local.Port.ToString(),
																		   " ",
																		   "",
																		   MyAPI.UdpExConnexion.table[i].dwProcessId.ToString(),
																		   MyAPI.UdpExConnexion.table[i].ProcessName

																	   }));
			}
			this.label1.Text="NB connexion = "+MyAPI.UdpExConnexion.dwNumEntries.ToString();
		}


		private void button7_Click(object sender, System.EventArgs e)
		{
			MyAPI.GetUdpStats();
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "In Datagrams",
																	   MyAPI.UdpStats.dwInDatagrams.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Out Datagrams",
																	   MyAPI.UdpStats.dwOutDatagrams.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "In Errors",
																	   MyAPI.UdpStats.dwInErrors.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "No Ports",
																	   MyAPI.UdpStats.dwNoPorts.ToString(),
																	   ""
																   }));
			this.listView1.Items.Add(new ListViewItem(new string[] {
																	   "Num Address",
																	   MyAPI.UdpStats.dwNumAddrs.ToString(),
																	   ""
																   }));
		}

		

		

		


	}
}
