using SimpleTCP;
using System.Text;

namespace I_mAboutToLunch
{
    public partial class IamGoingToHaveLunch : Form
    {
        public IamGoingToHaveLunch()
        {
            InitializeComponent();
        }

        private SimpleTcpClient client;

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            client.Connect(textBoxHost.Text, Convert.ToUInt16(textBoxPort.Text));
            buttonConnect.Enabled = false;
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            buttonConnect.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.ASCII;
            client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object? sender, SimpleTCP.Message message)
        {
            textBoxStatus.Invoke((MethodInvoker)delegate ()
            {
                textBoxStatus.Text += String.Format("{0}", message.MessageString.TrimEnd('\u0013')); 
            });
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            client.WriteLine(MessageCreator(buttonSend.Text));
        }

        private String MessageCreator(String inputMessage)
        {
            DateTime localDate = DateTime.Now;
            return String.Format("{0}: {1}", localDate.ToString(), inputMessage);
        }

    }
}