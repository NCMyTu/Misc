using AxWMPLib;
using chat;
using Microsoft.VisualBasic.ApplicationServices;
using System.Data;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace MyChat
{
    public partial class MyChat : Form
    {
        const string user_info_path = @"C:\Users\PC MY TU\Downloads\MyChat\data\users_info.txt";
        const string avatar_path = @"C:\Users\PC MY TU\Downloads\MyChat\data\avatar\";
        const string appPath = @"C:\Users\PC MY TU\Downloads\MyChat\";
        const string chat_history_path = @"C:\Users\PC MY TU\Downloads\MyChat\data\chat_history.txt";
        string preview_avatar_path = "";
        string mode = "light";
        string currentImagePath = "";
        string currentAvatarPath = "";
        int verticalPaddingMessage = 5;
        string currentUserID = "";
        string receiverID = "";
        Image currentSentImage;
        string currentSentDate = "";
        DataTable dt_user_info = new DataTable();
        DataTable dt_chat_history = new DataTable();

        public DataTable LoadInfo(string path)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("userID", typeof(string));
            dt.Columns.Add("username", typeof(string));
            dt.Columns.Add("password", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("avatarPath", typeof(string));

            string[] lines = File.ReadAllLines(path);

            if (lines.Length == 0)
                return dt;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] values = line.Split("*");

                DataRow row = dt.NewRow();

                row["userID"] = values[0];
                row["username"] = values[1];
                row["password"] = values[2];
                row["name"] = values[3];
                row["email"] = values[4];
                row["avatarPath"] = values[5];

                dt.Rows.Add(row);
            }

            return dt;
        }

        private void CenterForm()
        {
            // Calculate the center point of the screen
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;

            int centerX = (screenWidth - formWidth) / 2;
            int centerY = (screenHeight - formHeight) / 2;

            // Set the form's location to the center
            this.Location = new Point(centerX, centerY);
        }

        public DataTable LoadChatHistory(string path)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("senderID", typeof(string));
            dt.Columns.Add("receiverID", typeof(string));
            dt.Columns.Add("type", typeof(string));
            dt.Columns.Add("content", typeof(string));
            dt.Columns.Add("date", typeof(string));

            string[] lines = File.ReadAllLines(path);

            if (lines.Length == 0)
                return dt;

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] values = line.Split("*");

                DataRow row = dt.NewRow();

                row["senderID"] = values[0];
                row["receiverID"] = values[1];
                row["type"] = values[2];
                row["content"] = values[3];
                row["date"] = values[4];

                dt.Rows.Add(row);
            }

            return dt;
        }

        public int GetUserID(string username, string password)
        {
            DataRow[] matchingRow = dt_user_info.Select($"username = '{username}' AND password = '{password}'");

            if (matchingRow.Length == 0)
            {
                return -1;
            }

            int userId = int.Parse(matchingRow[0]["userID"].ToString());
            return userId;
        }

        public void SignIn(string userID)
        {
            this.currentUserID = userID;

            DataRow[] matchingRow = dt_user_info.Select($"userID = '{userID}'");

            currentAvatarPath = Path.Combine(avatar_path, matchingRow[0]["avatarPath"].ToString());

            SwitchToMyChat();

            DeleteComponents(pnl_chat_holder);
        }

        public void SwitchToMyChat()
        {
            pnl_MyChat.Visible = true;
            pnl_signin.Visible = false;
            pnl_signup.Visible = false;
            pnl_chat_holder.Visible = true;
            pnl_chat_holder.BringToFront();
            pnl_archive.Visible = false;
            pnl_archive.SendToBack();
            pnl_MyChat.BringToFront();

            this.ClientSize = new Size(pnl_MyChat.Width, pnl_MyChat.Height);
            pnl_MyChat.Location = new Point(0, 0);

            pic_avatar.Image = Image.FromFile(currentAvatarPath);
            pic_avatar_big.Image = Image.FromFile(currentAvatarPath);

            foreach (DataRow row in dt_user_info.Rows)
            {
                if (row["userID"].ToString() == currentUserID)
                {
                    lbl_name.Text = row["name"].ToString();
                    break;
                }
            }

            int x = Convert.ToInt16((double)pnl_signin_component_holder.Width / 2 - (double)lbl_name.Width / 2);
            lbl_name.Location = new Point(x, lbl_name.Location.Y);

            pnl_MyChat_chat.Visible = true;
            pnl_MyChat_setting.Visible = false;
            pnl_MyChat_user.Visible = false;

            LoadOtherUsers();

            txt_MyChat_message.TabIndex = 0;

            CenterForm();
        }

        public void SwitchToSignIn()
        {
            pnl_MyChat.Visible = false;
            pnl_signin.Visible = true;
            pnl_signup.Visible = false;
            pnl_signin.BringToFront();

            this.ClientSize = new Size(pnl_signin.Width, pnl_signin.Height);
            pnl_signin.Location = new Point(0, 0);

            txt_signin_username.Text = string.Empty;
            txt_signin_password.Text = string.Empty;

            txt_signin_username.TabIndex = 0;
            txt_signin_password.TabIndex = 1;
            btn_signin.TabIndex = 2;

            CenterForm();
        }

        public void SwitchToSignUp()
        {
            pnl_MyChat.Visible = false;
            pnl_signin.Visible = false;
            pnl_signup.Visible = true;
            pnl_signup.BringToFront();

            this.ClientSize = new Size(pnl_signup.Width, pnl_signup.Height);
            pnl_signup.Location = new Point(0, 0);

            txt_signup_email.Text = string.Empty;
            txt_signup_username.Text = string.Empty;
            txt_signup_password.Text = string.Empty;
            txt_signup_name.Text = string.Empty;
            textBox1.Text = string.Empty;

            lbl_signup_warning.Visible = false;
            preview_avatar_path = "";

            txt_signup_email.TabIndex = 0;
            txt_signup_username.TabIndex = 1;
            txt_signup_password.TabIndex = 2;
            txt_signup_name.TabIndex = 3;
            btn_register.TabIndex = 4;
            lbl_signup_signin.TabIndex = 5;

            CenterForm();
        }

        public void ScrollChatPanel(UserControl c, string position)
        {
            int x;
            if (position == "left")
                x = 0;
            else
                x = pnl_chat_holder.Width - c.Width;

            int y = pnl_chat_holder.Height - c.Height -10;
            c.Location = new Point(x, y+10);

            foreach (Control control in pnl_chat_holder.Controls)
            {
                if (control != c)
                {
                    x = control.Location.X;
                    y = control.Location.Y - c.Height - verticalPaddingMessage;
                    control.Location = new Point(x, y);
                }
            }

            pnl_chat_holder.VerticalScroll.Value = pnl_chat_holder.VerticalScroll.Maximum;

            pnl_chat_holder.Invalidate();
        }

        private string GetEmojiPath(string emojiName)
        {
            string path = Path.Combine(appPath, "assets", "emoji");
            string emojiFile;

            switch (emojiName)
            {
                case "slightly_smiling":
                    emojiFile = "slightly_smiling.png";
                    break;
                case "smiling_with_sweat":
                    emojiFile = "smiling_with_sweat.png";
                    break;
                case "heart_eyes":
                    emojiFile = "heart_eyes.png";
                    break;
                case "angry_devil":
                    emojiFile = "angry_devil.png";
                    break;
                case "face_with_cold_sweat":
                    emojiFile = "face_with_cold_sweat.png";
                    break;
                case "face_without_mouth":
                    emojiFile = "face_without_mouth.png";
                    break;
                case "very_angry":
                    emojiFile = "very_angry.png";
                    break;
                case "zipper_mouth":
                    emojiFile = "zipper_mouth.png";
                    break;
                case "tongue_out":
                    emojiFile = "tongue_out.png";
                    break;
                default:
                    emojiFile = "";
                    break;
            }

            return Path.Combine(path, emojiFile);
        }

        public void ToggleArchivePanel()
        {
            if (pnl_archive.Visible)
            {
                pnl_archive.Visible = false;
                pnl_archive.SendToBack();
                pnl_chat_holder.Visible = true;
                pnl_chat_holder.BringToFront();
            }
            else
            {
                pnl_archive.Visible = true;
                pnl_archive.BringToFront();
                pnl_chat_holder.Visible = false;
                pnl_chat_holder.SendToBack();
            }
        }

        private void AddChat(string message, string avatarPath, string position)
        {
            uct_chat chat = new uct_chat();

            chat.SetMessage(message, avatarPath);

            pnl_chat_holder.Controls.Add(chat);

            ScrollChatPanel(chat, position);
        }

        private void AddEmoji(string emojiName, string avatarPath, string position)
        {
            uct_emoji emoji = new uct_emoji();

            string emojiPath = GetEmojiPath(emojiName);
            emoji.SetEmoji(emojiPath, avatarPath);

            pnl_chat_holder.Controls.Add(emoji);

            ScrollChatPanel(emoji, position);
        }

        private void AddImage(string imagePath, string avatarPath, string position)
        {
            uct_image image = new uct_image();

            image.SetImage(imagePath, avatarPath);

            pnl_chat_holder.Controls.Add(image);

            ScrollChatPanel(image, position);
        }

        private void AddVideo(string videoPath, string avatarPath, string position)
        {
            uct_video video = new uct_video();

            video.SetVideo(videoPath, avatarPath);

            pnl_chat_holder.Controls.Add(video);

            ScrollChatPanel(video, position);
        }

        public string GetAvatarPathFromID(string userID)
        {
            foreach (DataRow row in dt_user_info.Rows)
            {
                if (row["userID"].ToString() == userID)
                {
                    return Path.Combine(avatar_path, row["avatarPath"].ToString());
                }
            }

            return "";
        }

        public void SaveChatHistory(string senderID, string receiverID, string type, string content)
        {
            DateTime currentDate = DateTime.Now;
            StreamWriter writer = new StreamWriter(chat_history_path, true);
            writer.WriteLine(senderID + "*" + receiverID + "*" + type + "*" + content + "*" + currentDate.ToString("dd-MM-yyyy"));
            writer.Close();
        }

        public MyChat()
        {
            InitializeComponent();
        }

        public void LoadOtherUsers()
        {
            Control[] controls = pnl_MyChat_chat.Controls.OfType<Control>().ToArray();

            foreach (Control c in controls)
            {
                pnl_chat_holder.Controls.Remove(c);
                c.Dispose();
            }

            int currentY = 30;
            foreach (DataRow row in dt_user_info.Rows)
            {
                if (row["userID"].ToString() != currentUserID)
                {
                    string id = row["userID"].ToString();
                    string name = row["name"].ToString();
                    string avatarPath = Path.Combine(avatar_path, row["avatarPath"].ToString());

                    uct_user user = new uct_user();
                    user.SetUserInfo(id, name, avatarPath, "");

                    user.Location = new Point(0, currentY);
                    currentY += user.Height + 10;
                    user.Click += uct_user_Click_LoadChat;

                    pnl_MyChat_chat.Controls.Add(user);
                }
            }
        }

        public void DeleteComponents(Panel panel)
        {
            if (panel == pnl_chat_holder)
            {
                Control[] controls = panel.Controls.OfType<Control>().ToArray();

                foreach (Control c in controls)
                {
                    panel.Controls.Remove(c);
                    c.Dispose();
                }
            }
            else
            {
                Control[] controls = panel.Controls.OfType<Control>().ToArray();

                foreach (Control c in controls)
                {
                    if (c != lbl_archive_image && c != lbl_archive_video)
                    {
                        panel.Controls.Remove(c);
                        c.Dispose();
                    }
                }
            }
        }

        public void LoadChatPerType(DataRow row, string senderAvatarPath)
        {
            string type = row["type"].ToString();
            string content = row["content"].ToString();

            if (type == "chat")
            {
                AddChat(content, senderAvatarPath, "right");
            }
            else if (type == "emoji")
            {
                AddEmoji(content, senderAvatarPath, "right");
            }
            else if (type == "image")
            {
                AddImage(content, senderAvatarPath, "right");
            }
            else
            {
                AddVideo(content, senderAvatarPath, "right");
            }
        }

        public void uct_user_Click_LoadChat(object sender, EventArgs e)
        {
            DeleteComponents(pnl_chat_holder);

            dt_chat_history = LoadChatHistory(chat_history_path);

            uct_user user = (uct_user)sender;
            receiverID = user.GetUserID();

            foreach (uct_user u in pnl_MyChat_chat.Controls.OfType<uct_user>())
            {
                if (u != user)
                    u.UndarkenBackground();
            }
            user.DarkenBackground();


            foreach (DataRow row in dt_chat_history.Rows)
            {
                if (row["senderID"].ToString() == currentUserID && row["receiverID"].ToString() == receiverID)
                {
                    LoadChatPerType(row, currentAvatarPath);
                    //AddChat(row["content"].ToString(), currentAvatarPath, "right");
                }
                else if (row["senderID"].ToString() == receiverID && row["receiverID"].ToString() == currentUserID)
                {
                    LoadChatPerType(row, GetAvatarPathFromID(row["senderID"].ToString()));
                    //AddChat(row["content"].ToString(), GetAvatarPathFromID(row["senderID"].ToString()), "right");
                }
            }

            if (mode == "light")
            {
                foreach (uct_chat c in pnl_chat_holder.Controls.OfType<uct_chat>().ToArray())
                {
                    c.SwitchToLightMode();
                }

                foreach (uct_emoji c in pnl_chat_holder.Controls.OfType<uct_emoji>().ToArray())
                {
                    c.SwitchToLightMode();
                }

                foreach (uct_image c in pnl_chat_holder.Controls.OfType<uct_image>().ToArray())
                {
                    c.SwitchToLightMode();
                }

                foreach (uct_video c in pnl_chat_holder.Controls.OfType<uct_video>().ToArray())
                {
                    c.SwitchToLightMode();
                }
            }
            else
            {
                foreach (uct_chat c in pnl_chat_holder.Controls.OfType<uct_chat>().ToArray())
                {
                    c.SwitchToDarkMode();
                }

                foreach (uct_emoji c in pnl_chat_holder.Controls.OfType<uct_emoji>().ToArray())
                {
                    c.SwitchToDarkMode();
                }

                foreach (uct_image c in pnl_chat_holder.Controls.OfType<uct_image>().ToArray())
                {
                    c.SwitchToDarkMode();
                }

                foreach (uct_video c in pnl_chat_holder.Controls.OfType<uct_video>().ToArray())
                {
                    c.SwitchToDarkMode();
                }
            }

            DeleteComponents(pnl_archive);

            int x_image = 65, y_image = 75;
            int x_video = 65, y_video = 325;
            // get images
            foreach (DataRow row in dt_chat_history.Rows)
            {
                if (row["type"].ToString() == "image" &&
                    row["senderID"].ToString() == currentUserID && row["receiverID"].ToString() == receiverID)
                {
                    PictureBox image_sent = new PictureBox();

                    image_sent.Location = new Point(x_image, y_image);
                    image_sent.Size = new Size(180, 180);
                    image_sent.Image = Image.FromFile(row["content"].ToString());
                    image_sent.SizeMode = PictureBoxSizeMode.StretchImage;
                    image_sent.Click += (sender, e) => DisplaySentImage(sender, e, row["date"].ToString());

                    pnl_archive.Controls.Add(image_sent);

                    x_image += image_sent.Width + 30;
                }
                else if (row["type"].ToString() == "video" &&
                    row["senderID"].ToString() == currentUserID && row["receiverID"].ToString() == receiverID)
                {
                    AxWindowsMediaPlayer video_sent = new AxWindowsMediaPlayer();

                    // fking dumb, must add to parent control first,
                    // otherwise causes a disaster
                    pnl_archive.Controls.Add(video_sent);

                    video_sent.Location = new Point(x_video, y_video);
                    video_sent.Size = new Size(180, 180);
                    video_sent.settings.autoStart = false;
                    video_sent.stretchToFit = true;
                    video_sent.URL = row["content"].ToString();
                    // must set uiMode to full, otherwise cause another disaster
                    video_sent.uiMode = "mini";

                    Label lbl_date = new Label();
                    lbl_date.Location = new Point(x_video, video_sent.Location.Y + video_sent.Height + 10);
                    lbl_date.Width = video_sent.Width;
                    lbl_date.Text = row["date"].ToString();
                    lbl_date.TextAlign = ContentAlignment.MiddleCenter;

                    pnl_archive.Controls.Add(lbl_date);

                    lbl_date.BringToFront();

                    x_video += video_sent.Width + 30;
                    // nvm, when this panel is off disaster says hi
                }
            }
        }

        public void MyChat_Load(object sender, EventArgs e)
        {
            dt_user_info = LoadInfo(user_info_path);
            dt_chat_history = LoadChatHistory(chat_history_path);

            SwitchToSignIn();

            pnl_chat_holder.AutoScroll = true;

            CenterForm();
        }

        private void btn_signin_Click(object sender, EventArgs e)
        {
            string username = txt_signin_username.Text, password = txt_signin_password.Text;
            int userID = GetUserID(username, password);

            if (userID == -1)
                lbl_signin_warning.Visible = true;
            else
                SignIn(userID.ToString());
        }

        private void lbl_signup_Click(object sender, EventArgs e)
        {
            SwitchToSignUp();
        }

        private void lbl_forgot_password_Click(object sender, EventArgs e)
        {
            ForgotPassword f = new ForgotPassword();
            f.ShowDialog();
        }

        private void lbl_signup_browse_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pic_signup_avatar.Image = System.Drawing.Image.FromFile(fileDialog.FileName);
                pic_signup_avatar.SizeMode = PictureBoxSizeMode.StretchImage;
                preview_avatar_path = fileDialog.FileName;
            }
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            DataRow[] matchingRow = dt_user_info.Select($"username = '{txt_signup_username.Text}'");

            if (string.IsNullOrWhiteSpace(txt_signup_email.Text) || string.IsNullOrWhiteSpace(txt_signup_username.Text) ||
                string.IsNullOrWhiteSpace(txt_signup_password.Text) || string.IsNullOrWhiteSpace(txt_signup_name.Text))
            {
                lbl_signup_warning.Text = "Please fill in all the required fields";
                int x = Convert.ToInt16((double)pnl_signin_component_holder.Width / 2 - (double)lbl_signup_warning.Width / 2);
                lbl_signup_warning.Location = new Point(x, lbl_signup_warning.Location.Y);
                lbl_signup_warning.Visible = true;
            }
            else if (preview_avatar_path == "")
            {
                preview_avatar_path = @"C:\Users\PC MY TU\Downloads\MyChat\assets\avatar.png";

                int userID;

                if (dt_user_info.Rows.Count == 0)
                {
                    userID = 0;
                }
                else
                {
                    userID = int.Parse(dt_user_info.Rows[dt_user_info.Rows.Count - 1][0].ToString());
                    userID += 1;
                }

                // copy avatar to data\avatar, rename it to {userID}.{extension}
                string desPath = Path.Combine(avatar_path, Path.GetFileName(preview_avatar_path));
                File.Copy(preview_avatar_path, desPath, true);

                string newAvatarName = userID.ToString() + Path.GetExtension(preview_avatar_path);
                File.Move(desPath, Path.Combine(avatar_path, newAvatarName));

                // write data
                StreamWriter writer = new StreamWriter(user_info_path, true);
                writer.WriteLine(userID.ToString() + "*" + txt_signup_username.Text + "*" + txt_signup_password.Text
                                 + "*" + txt_signup_name.Text + "*" + txt_signup_email.Text + "*" + newAvatarName);
                writer.Close();

                //update data
                dt_user_info = LoadInfo(user_info_path);

                lbl_signup_warning.Text = "Register user successfully";
                lbl_signup_warning.ForeColor = System.Drawing.Color.Green;
                int x = Convert.ToInt16((double)pnl_signin_component_holder.Width / 2 - (double)lbl_signup_warning.Width / 2);
                lbl_signup_warning.Location = new Point(x, lbl_signup_warning.Location.Y);
                lbl_signup_warning.Visible = true;
            }
            else if (matchingRow.Length > 0)
            {
                lbl_signup_warning.Text = "Username already exists";
                int x = Convert.ToInt16((double)pnl_signin_component_holder.Width / 2 - (double)lbl_signup_warning.Width / 2);
                lbl_signup_warning.Location = new Point(x, lbl_signup_warning.Location.Y);
                lbl_signup_warning.Visible = true;
            }
            else
            {
                int userID;

                if (dt_user_info.Rows.Count == 0)
                {
                    userID = 0;
                }
                else
                {
                    userID = int.Parse(dt_user_info.Rows[dt_user_info.Rows.Count - 1][0].ToString());
                    userID += 1;
                }

                // copy avatar to data\avatar, rename it to {userID}.{extension}
                string desPath = Path.Combine(avatar_path, Path.GetFileName(preview_avatar_path));
                File.Copy(preview_avatar_path, desPath, true);

                string newAvatarName = userID.ToString() + Path.GetExtension(preview_avatar_path);
                File.Move(desPath, Path.Combine(avatar_path, newAvatarName));

                // write data
                StreamWriter writer = new StreamWriter(user_info_path, true);
                writer.WriteLine(userID.ToString() + "*" + txt_signup_username.Text + "*" + txt_signup_password.Text
                                 + "*" + txt_signup_name.Text + "*" + txt_signup_email.Text + "*" + newAvatarName);
                writer.Close();

                //update data
                dt_user_info = LoadInfo(user_info_path);

                lbl_signup_warning.Text = "Register user successfully";
                lbl_signup_warning.ForeColor = System.Drawing.Color.Green;
                int x = Convert.ToInt16((double)pnl_signin_component_holder.Width / 2 - (double)lbl_signup_warning.Width / 2);
                lbl_signup_warning.Location = new Point(x, lbl_signup_warning.Location.Y);
                lbl_signup_warning.Visible = true;
            }
        }

        private void lbl_signup_signin_Click(object sender, EventArgs e)
        {
            SwitchToSignIn();
        }

        private void pic_send_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txt_MyChat_message.Text))
            {
                string message = txt_MyChat_message.Text;
                AddChat(message, currentAvatarPath, "right");
                SaveChatHistory(currentUserID, receiverID, "chat", message);
            }

            txt_MyChat_message.Text = string.Empty;
        }

        private void txt_MyChat_message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (!String.IsNullOrWhiteSpace(txt_MyChat_message.Text))
                {
                    string message = txt_MyChat_message.Text;
                    AddChat(message, currentAvatarPath, "right");
                    SaveChatHistory(currentUserID, receiverID, "chat", message);
                }

                txt_MyChat_message.Text = string.Empty;
            }
        }

        private void txt_signin_username_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string username = txt_signin_username.Text, password = txt_signin_password.Text;
                int userID = GetUserID(username, password);

                if (userID == -1)
                    lbl_signin_warning.Visible = true;
                else
                    SignIn(userID.ToString());
            }
        }

        private void txt_signin_password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string username = txt_signin_username.Text, password = txt_signin_password.Text;
                int userID = GetUserID(username, password);

                if (userID == -1)
                    lbl_signin_warning.Visible = true;
                else
                    SignIn(userID.ToString());
            }
        }

        private void pic_emoji_Click(object sender, EventArgs e)
        {
            if (pnl_emoji_holder.Visible == false)
            {
                pnl_emoji_holder.Visible = true;
                pnl_emoji_holder.BringToFront();
            }
            else
            {
                pnl_emoji_holder.Visible = false;
                pnl_emoji_holder.SendToBack();
            }
        }

        private void pic_emoji_slightly_smiling_Click(object sender, EventArgs e)
        {
            AddEmoji("slightly_smiling", currentAvatarPath, "right");
            SaveChatHistory(currentUserID, receiverID, "emoji", "slightly_smiling");
        }

        private void pic_emoji_smiling_with_sweat_Click(object sender, EventArgs e)
        {
            AddEmoji("smiling_with_sweat", currentAvatarPath, "right");
            SaveChatHistory(currentUserID, receiverID, "emoji", "smiling_with_sweat");
        }

        private void pic_emoji_heart_eyes_Click(object sender, EventArgs e)
        {
            AddEmoji("heart_eyes", currentAvatarPath, "right");
            SaveChatHistory(currentUserID, receiverID, "emoji", "heart_eyes");
        }

        private void pic_face_with_cold_sweat_Click(object sender, EventArgs e)
        {
            AddEmoji("face_with_cold_sweat", currentAvatarPath, "right");
            SaveChatHistory(currentUserID, receiverID, "emoji", "face_with_cold_sweat");
        }

        private void pic_emoji_tongue_out_Click(object sender, EventArgs e)
        {
            AddEmoji("tongue_out", currentAvatarPath, "right");
            SaveChatHistory(currentUserID, receiverID, "emoji", "tongue_out");
        }

        private void pic_emoji_face_without_mouth_Click(object sender, EventArgs e)
        {
            AddEmoji("face_without_mouth", currentAvatarPath, "right");
            SaveChatHistory(currentUserID, receiverID, "emoji", "face_without_mouth");
        }

        private void pic_emoji_zipper_mouth_Click(object sender, EventArgs e)
        {
            AddEmoji("zipper_mouth", currentAvatarPath, "right");
            SaveChatHistory(currentUserID, receiverID, "emoji", "zipper_mouth");
        }

        private void pic_emoji_very_angry_Click(object sender, EventArgs e)
        {
            AddEmoji("very_angry", currentAvatarPath, "right");
            SaveChatHistory(currentUserID, receiverID, "emoji", "very_angry");
        }

        private void pic_emoji_angry_devil_Click(object sender, EventArgs e)
        {
            AddEmoji("angry_devil", currentAvatarPath, "right");
            SaveChatHistory(currentUserID, receiverID, "emoji", "angry_devil");
        }

        private void pic_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files (*.jpg; *.jpeg; *.png; *bmp)|*.jpg; *.jpeg; *.png; *bmp|All files (*.*)|*.*";
            open.Multiselect = true;

            if (open.ShowDialog() == DialogResult.OK)
            {
                foreach (string filePath in open.FileNames)
                {
                    currentImagePath = filePath;
                    AddImage(currentImagePath, currentAvatarPath, "right");
                    SaveChatHistory(currentUserID, receiverID, "image", currentImagePath);
                }
            }
        }

        private void pic_MyChat_chat_Click(object sender, EventArgs e)
        {
            pnl_MyChat_chat.Visible = true;
            pnl_MyChat_setting.Visible = false;
            pnl_MyChat_user.Visible = false;
        }

        private void pic_MyChat_setting_Click(object sender, EventArgs e)
        {
            pnl_MyChat_chat.Visible = false;
            pnl_MyChat_setting.Visible = true;
            pnl_MyChat_user.Visible = false;
        }

        private void pic_search_text_Click(object sender, EventArgs e)
        {
            if (txt_search.Visible)
            {
                txt_search.Visible = false;
                txt_search.Text = string.Empty;
            }
            else
            {
                txt_search.Visible = true;
                txt_search.Text = string.Empty;
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txt_search.Text))
            {
                string textToSearch = txt_search.Text;

                foreach (uct_chat chat in pnl_chat_holder.Controls.OfType<uct_chat>())
                    chat.SelectText(textToSearch);
            }
            else
            {
                foreach (uct_chat chat in pnl_chat_holder.Controls.OfType<uct_chat>())
                    chat.Deselect();
            }

            txt_search.Focus();
        }

        private void pic_video_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "MP3, MP4 files (*.mp3; *.mp4)|*.mp3; *.mp4|All files (*.*)|*.*";
            open.Multiselect = true;

            if (open.ShowDialog() == DialogResult.OK)
            {
                foreach (string videoPath in open.FileNames)
                {
                    currentImagePath = videoPath;
                    AddVideo(currentImagePath, currentAvatarPath, "right");
                    SaveChatHistory(currentUserID, receiverID, "video", currentImagePath);
                }
            }
        }

        private void lbl_logout_Click(object sender, EventArgs e)
        {
            SwitchToSignIn();
            pnl_emoji_holder.Visible = false;
            lbl_signin_warning.Visible = false;
        }

        private void pic_avatar_Click(object sender, EventArgs e)
        {
            pnl_MyChat_chat.Visible = false;
            pnl_MyChat_setting.Visible = false;
            pnl_MyChat_user.Visible = true;
        }

        public void DeteleBlackPanel(object sender, EventArgs e)
        {
            Panel panel = (Panel)sender;
            Panel parentPanel = (Panel)panel.Parent;

            foreach (Control c in panel.Controls)
            {
                panel.Controls.Remove(c);
                c.Dispose();
            }

            parentPanel.Controls.Remove(panel);
            panel.Dispose();
        }

        public void DisplaySentImage(object sender, EventArgs e, string date)
        {
            PictureBox imgBox = (PictureBox)sender;
            currentSentImage = imgBox.Image;

            Panel pnl_background = new Panel();

            pnl_background.Size = pnl_MyChat.Size;
            pnl_background.Location = new Point(0, 0);
            pnl_background.BackColor = Color.Black;
            pnl_background.Click += DeteleBlackPanel;

            imgBox = new PictureBox();
            imgBox.Size = new Size(1400, 600);
            imgBox.Image = currentSentImage;

            if (imgBox.Image.Width <= imgBox.Width && imgBox.Image.Height <= imgBox.Height)
            {
                imgBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                Image image = imgBox.Image;

                double wFactor = (double)image.Width / imgBox.Width;
                double hFactor = (double)image.Height / imgBox.Height;
                double resizeFactor = Math.Max(wFactor, hFactor);

                int newWidth = Convert.ToInt16(image.Width / resizeFactor);
                int newHeight = Convert.ToInt16(image.Height / resizeFactor);

                imgBox.Size = new Size(newWidth, newHeight);

                imgBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            int x = (pnl_background.Width - imgBox.Width) / 2;
            int y = (pnl_background.Height - imgBox.Height) / 2;
            imgBox.Location = new Point(x, y);
            pnl_background.Controls.Add(imgBox);

            Label lbl_date = new Label();
            lbl_date.AutoSize = true;
            lbl_date.Text = date;
            lbl_date.Font = new Font("Segoe UI", 12);
            x = Convert.ToInt16((pnl_background.Width - lbl_date.Width) / 2);
            y = imgBox.Location.Y + imgBox.Height + 10;
            lbl_date.Location = new Point(x, y);
            lbl_date.ForeColor = Color.White;
            pnl_background.Controls.Add(lbl_date);

            pnl_MyChat.Controls.Add(pnl_background);

            pnl_background.BringToFront();
        }

        private void pic_archive_Click(object sender, EventArgs e)
        {
            ToggleArchivePanel();

            DeleteComponents(pnl_archive);

            int x_image = 65, y_image = 75;
            int x_video = 65, y_video = 325;
            // get images
            foreach (DataRow row in dt_chat_history.Rows)
            {
                if (row["type"].ToString() == "image" &&
                    row["senderID"].ToString() == currentUserID && row["receiverID"].ToString() == receiverID)
                {
                    PictureBox image_sent = new PictureBox();

                    image_sent.Location = new Point(x_image, y_image);
                    image_sent.Size = new Size(180, 180);
                    image_sent.Image = Image.FromFile(row["content"].ToString());
                    image_sent.SizeMode = PictureBoxSizeMode.StretchImage;
                    image_sent.Click += (sender, e) => DisplaySentImage(sender, e, row["date"].ToString());

                    pnl_archive.Controls.Add(image_sent);

                    x_image += image_sent.Width + 30;
                }
                else if (row["type"].ToString() == "video" &&
                    row["senderID"].ToString() == currentUserID && row["receiverID"].ToString() == receiverID)
                {
                    AxWindowsMediaPlayer video_sent = new AxWindowsMediaPlayer();

                    // fking dumb, must add to parent control first,
                    // otherwise causes a disaster
                    pnl_archive.Controls.Add(video_sent);

                    video_sent.Location = new Point(x_video, y_video);
                    video_sent.Size = new Size(180, 180);
                    video_sent.settings.autoStart = false;
                    video_sent.stretchToFit = true;
                    video_sent.URL = row["content"].ToString();
                    // must set uiMode to full, otherwise cause another disaster
                    video_sent.uiMode = "mini";

                    Label lbl_date = new Label();
                    lbl_date.Location = new Point(x_video, video_sent.Location.Y + video_sent.Height + 10);
                    lbl_date.Width = video_sent.Width;
                    lbl_date.Text = row["date"].ToString();
                    lbl_date.TextAlign = ContentAlignment.MiddleCenter;

                    pnl_archive.Controls.Add(lbl_date);

                    lbl_date.BringToFront();

                    x_video += video_sent.Width + 30;
                    // nvm, when this panel is off disaster says hi
                }
            }
        }

        private void rad_theme_light_CheckedChanged(object sender, EventArgs e)
        {
            mode = "light";

            pnl_MyChat.BackColor = Color.White;

            pnl_chat_holder.BackColor = Color.White;
            pnl_misc.BackColor = Color.White;

            pic_MyChat_setting.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\setting_light.png");
            pic_MyChat_setting.BackColor = Color.White;
            lbl_MyChat_setting.ForeColor = Color.FromArgb(38, 46, 53);

            pic_MyChat_chat.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\chat_light.png");
            pic_MyChat_chat.BackColor = Color.White;
            lbl_MyChat_chat.ForeColor = Color.FromArgb(38, 46, 53);

            pic_emoji.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\emoji_light.png");
            pic_emoji.BackColor = Color.White;

            pic_video.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\paperclip_light.png");
            pic_video.BackColor = Color.White;

            pic_image.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\image_light.png");
            pic_image.BackColor = Color.White;

            pic_send.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\send_light.png");
            pic_send.BackColor = Color.White;

            pic_search_text.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\search_light.png");
            pic_search_text.BackColor = Color.White;

            pic_archive.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\archive_light.png");
            pic_archive.BackColor = Color.White;

            lbl_archive_image.ForeColor = Color.Black;
            lbl_archive_video.ForeColor = Color.Black;

            foreach (uct_chat c in pnl_chat_holder.Controls.OfType<uct_chat>().ToArray())
            {
                c.SwitchToLightMode();
            }

            foreach (uct_emoji c in pnl_chat_holder.Controls.OfType<uct_emoji>().ToArray())
            {
                c.SwitchToLightMode();
            }

            foreach (uct_image c in pnl_chat_holder.Controls.OfType<uct_image>().ToArray())
            {
                c.SwitchToLightMode();
            }

            foreach (uct_video c in pnl_chat_holder.Controls.OfType<uct_video>().ToArray())
            {
                c.SwitchToLightMode();
            }

            pnl_signin.BackColor = Color.FromArgb(200, 230, 255);
            pnl_signup.BackColor = Color.FromArgb(200, 230, 255);

            lbl_app_name.ForeColor = Color.Black;
            lbl_signin.ForeColor = Color.Black;
            lbl_signin_to_continue.ForeColor = Color.Black;
            lbl_dont_have_acc.ForeColor = Color.Black;

            lbl_signup_app_name.ForeColor = Color.Black;
            lbl_register.ForeColor = Color.Black;
            lbl_signup_get_acc.ForeColor = Color.Black;
            lbl_have_acc.ForeColor = Color.Black;

            pic_app_icon.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\ico_light.png");
            pic_app_signup.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\ico_light.png");
        }

        private void rad_theme_dark_CheckedChanged(object sender, EventArgs e)
        {
            mode = "dark";

            pnl_MyChat.BackColor = Color.FromArgb(38, 46, 53);

            pnl_chat_holder.BackColor = Color.FromArgb(38, 46, 53);
            pnl_misc.BackColor = Color.FromArgb(38, 46, 53);

            pic_MyChat_setting.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\setting_dark.png");
            pic_MyChat_setting.BackColor = Color.FromArgb(38, 46, 53);
            lbl_MyChat_setting.ForeColor = Color.White;

            pic_MyChat_chat.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\chat_dark.png");
            pic_MyChat_chat.BackColor = Color.FromArgb(38, 46, 53);
            lbl_MyChat_chat.ForeColor = Color.White;

            pic_emoji.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\emoji_dark.png");
            pic_emoji.BackColor = Color.FromArgb(38, 46, 53);

            pic_video.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\paperclip_dark.png");
            pic_video.BackColor = Color.FromArgb(38, 46, 53);

            pic_image.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\image_dark.png");
            pic_image.BackColor = Color.FromArgb(38, 46, 53);

            pic_send.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\send_dark.png");
            pic_send.BackColor = Color.FromArgb(38, 46, 53);

            pic_search_text.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\search_dark.png");
            pic_search_text.BackColor = Color.FromArgb(38, 46, 53);

            pic_archive.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\archive_dark.png");
            pic_archive.BackColor = Color.FromArgb(38, 46, 53);

            lbl_archive_image.ForeColor = Color.White;
            lbl_archive_video.ForeColor = Color.White;

            foreach (uct_chat c in pnl_chat_holder.Controls.OfType<uct_chat>().ToArray())
            {
                c.SwitchToDarkMode();
            }

            foreach (uct_emoji c in pnl_chat_holder.Controls.OfType<uct_emoji>().ToArray())
            {
                c.SwitchToDarkMode();
            }

            foreach (uct_image c in pnl_chat_holder.Controls.OfType<uct_image>().ToArray())
            {
                c.SwitchToDarkMode();
            }

            foreach (uct_video c in pnl_chat_holder.Controls.OfType<uct_video>().ToArray())
            {
                c.SwitchToDarkMode();
            }

            pnl_signin.BackColor = Color.FromArgb(38, 46, 53);
            pnl_signup.BackColor = Color.FromArgb(38, 46, 53);

            lbl_app_name.ForeColor = Color.White;
            lbl_signin.ForeColor = Color.White;
            lbl_signin_to_continue.ForeColor = Color.White;
            lbl_dont_have_acc.ForeColor = Color.White;

            lbl_signup_app_name.ForeColor = Color.White;
            lbl_register.ForeColor = Color.White;
            lbl_signup_get_acc.ForeColor = Color.White;
            lbl_have_acc.ForeColor = Color.White;

            pic_app_icon.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\ico_dark.png");
            pic_app_signup.Image = Image.FromFile(@"C:\Users\PC MY TU\Downloads\MyChat\assets\ico_dark.png");
        }

        private void rad_language_en_CheckedChanged(object sender, EventArgs e)
        {
            lbl_archive_image.Text = "Images";
            lbl_archive_video.Text = "Videos";

            lbl_setting_theme.Text = "Theme";
            rad_theme_light.Text = "Light";
            rad_theme_dark.Text = "Dark";

            lbl_setting_language.Text = "Language";
            rad_language_en.Text = "English";
            rad_language_vi.Text = "Vietnamese";

            lbl_MyChat_chat.Text = "Chat";
            lbl_MyChat_setting.Text = "Setting";

            lbl_logout.Text = "Log out";

            int x = (pnl_MyChat_user.Width - lbl_logout.Width) / 2;
            int y = lbl_logout.Location.Y;
            lbl_logout.Location = new Point(x, y);

            x = (121 - lbl_MyChat_chat.Width) / 2;
            y = lbl_MyChat_chat.Location.Y;
            lbl_MyChat_chat.Location = new Point(x, y);

            x = (121 - lbl_MyChat_setting.Width) / 2;
            y = lbl_MyChat_setting.Location.Y;
            lbl_MyChat_setting.Location = new Point(x, y);

            lbl_signin.Text = "Sign in";
            lbl_signin.Location = new Point((pnl_signin.Width - lbl_signin.Width) / 2, lbl_signin.Location.Y);
            lbl_signin_to_continue.Text = "Sign in to continue to MyChat";
            lbl_username.Text = "Username";
            lbl_password.Text = "Password";
            lbl_forgot_password.Text = "Forgot password?";
            lbl_dont_have_acc.Text = "Don't have an account?";
            lbl_dont_have_acc.Location = new Point(157, 614);
            lbl_signup.Text = "Sign up";

            lbl_signup_phonenum.Text = "Phone number";
            lbl_register.Text = "Register";
            lbl_register.Location = new Point((pnl_signup.Width - lbl_register.Width) / 2, lbl_register.Location.Y);
            lbl_signup_get_acc.Text = "Get your MyChat account now";
            lbl_signup_get_acc.Location = new Point((pnl_signup.Width - lbl_signup_get_acc.Width) / 2, lbl_signup_get_acc.Location.Y);
            lbl_signup_username.Text = "Username";
            lbl_signup_password.Text = "Password";
            lbl_signup_name.Text = "Name";
            lbl_signup_avatar.Text = "Choose your avatar";
            lbl_signup_browse.Text = "Browse";
            lbl_have_acc.Text = "Already have an account?";
            lbl_have_acc.Location = new Point(145, 751);
            lbl_signup_signin.Text = "Sign in";
            lbl_signup_signin.Location = new Point(373, 751);
        }

        private void rad_language_vi_CheckedChanged(object sender, EventArgs e)
        {
            lbl_archive_image.Text = "Hình ảnh";
            lbl_archive_video.Text = "Video";

            lbl_setting_theme.Text = "Nền";
            rad_theme_light.Text = "Sáng";
            rad_theme_dark.Text = "Tối";

            lbl_setting_language.Text = "Ngôn ngữ";
            rad_language_en.Text = "Tiếng Anh";
            rad_language_vi.Text = "Tiếng Việt";

            lbl_MyChat_chat.Text = "Trò chuyện";
            lbl_MyChat_setting.Text = "Cài đặt";

            lbl_logout.Text = "Đăng xuất";

            int x = (pnl_MyChat_user.Width - lbl_logout.Width) / 2;
            int y = lbl_logout.Location.Y;
            lbl_logout.Location = new Point(x, y);

            x = (121 - lbl_MyChat_chat.Width) / 2;
            y = lbl_MyChat_chat.Location.Y;
            lbl_MyChat_chat.Location = new Point(x, y);

            x = (121 - lbl_MyChat_setting.Width) / 2;
            y = lbl_MyChat_setting.Location.Y;
            lbl_MyChat_setting.Location = new Point(x, y);

            lbl_signin.Text = "Đăng nhập";
            lbl_signin.Location = new Point((pnl_signin.Width - lbl_signin.Width)/2, lbl_signin.Location.Y);
            lbl_signin_to_continue.Text = "Đăng nhập để tiếp tục vào MyChat";
            lbl_username.Text = "Tên đăng nhập";
            lbl_password.Text = "Mật khẩu";
            lbl_forgot_password.Text = "Quên mật khẩu?";
            lbl_dont_have_acc.Text = "Không có tài khoản?";
            lbl_dont_have_acc.Location = new Point(lbl_dont_have_acc.Location.X + 20, lbl_dont_have_acc.Location.Y);
            lbl_signup.Text = "Đăng kí";

            lbl_signup_phonenum.Text = "Số điện thoại";
            lbl_register.Text = "Đăng kí";
            lbl_register.Location = new Point((pnl_signup.Width - lbl_register.Width) / 2, lbl_register.Location.Y);
            lbl_signup_get_acc.Text = "Tạo tài khoản MyChat ngay";
            lbl_signup_get_acc.Location = new Point((pnl_signup.Width - lbl_signup_get_acc.Width) / 2, lbl_signup_get_acc.Location.Y);
            lbl_signup_username.Text = "Tên đăng nhập";
            lbl_signup_password.Text = "Mật khẩu";
            lbl_signup_name.Text = "Tên";
            lbl_signup_avatar.Text = "Chọn avatar";
            lbl_signup_browse.Text = "Chọn";
            lbl_have_acc.Text = "Đã có tài khoản?";
            lbl_have_acc.Location = new Point(lbl_have_acc.Location.X + 50, lbl_have_acc.Location.Y);
            lbl_signup_signin.Text = "Đăng nhập";
            lbl_signup_signin.Location = new Point(lbl_signup_signin.Location.X - 20, lbl_signup_signin.Location.Y);
        }
    }
}
