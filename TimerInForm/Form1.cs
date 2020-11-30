using System;
using System.Threading;
using System.Windows.Forms;

namespace TimerInForm
{
	public partial class Form1 : Form
	{
		private readonly SafeTimerDecorator _timer;

		public Form1()
		{
			InitializeComponent();
			_timer = new SafeTimerDecorator(ShowTime, null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			_timer.Change(TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
		}

		private void button2_Click(object sender, EventArgs e)
		{
			_timer.Stop();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			_timer?.Dispose();
		}

		private void ShowTime(object state)
		{
			Invoke((MethodInvoker)(() =>
			{
				label1.Text = DateTime.Now.ToString("HH:mm:ss.fff tt");

			}));
		}


	}
}
