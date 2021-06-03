using System;
using System.Windows.Forms;

namespace BaseballGame
{
    public partial class Form1 : Form
    {
        public int max = 7; //기회
        int randomNum; //난수
        int success = 0; //성공횟수
        int fail = 0; //실패횟수
        int cnt = 0; //시도한 횟수
        bool randomStart = false; //난수의 생성 유무

        public Form1()
        {
            InitializeComponent();

            textBox2.Focus();
            textBox3.Text = "규칙 : " + max + "번 안으로 정답 맞추면 성공!! 아니면 실패!!";

            button1.Click += Button1_Click; //시작하기
            button2.Click += Button2_Click; //정답보기
            button3.Click += Button3_Click; //초기화
            button4.Click += Button4_Click; //결과확인
            button5.Click += Button5_Click; //관리

            label2.Text = success.ToString();
            label3.Text = fail.ToString();

            
        }

        private bool overlapCheck(String str) //입력한 숫자, 난수 중복 검사
        {
            String[] _str = new String[4];

            for (int i = 0; i < _str.Length; i++)   //하나씩 잘라서 넣기
            {
                _str[i] = str.Substring(i, 1);
            }

            for (int i = 0; i < _str.Length; i++)
            {
                for (int j = 0; j < _str.Length; j++)
                {
                    if (i != j)
                    {
                        if (_str[i].Equals(_str[j]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private String resultCheck(string randomNum, string inputNum) //결과 체크 함수
        {
            int strikeNum = 0;
            int ballNum = 0;

            String str = " / OUT!!";

            //정답과 입력된 숫자를 한자리씩 배열로 나눈다.
            String[] arrayRandomNum = new String[4];
            String[] arrayInput = new String[4];

            for (int i = 0; i < arrayRandomNum.Length; i++)
            {
                arrayRandomNum[i] = randomNum.Substring(i, 1);
            }

            for (int i = 0; i < arrayInput.Length; i++)
            {
                arrayInput[i] = inputNum.Substring(i, 1);
            }


            for (int i = 0; i < arrayInput.Length; i++)
            {
                for (int j = 0; j < arrayRandomNum.Length; j++)
                {
                    if (arrayInput[i].Equals(arrayRandomNum[j]))  //같은 숫자가 있는지 판단.
                    {
                        if (i == j) //자리수가 같으면 스트라이크, 자리수 다르면 볼
                        {
                            strikeNum++;
                        }
                        else
                        {
                            ballNum++;
                        }
                    }
                }
            }

            if (strikeNum != 0 || ballNum != 0)     // 체크결과 반환
            {
                str = " / 스트라이크 : " + strikeNum + " / 볼 : " + ballNum;
            }

            if(cnt == max)
            {
                if (strikeNum.Equals(4))
                {
                    success++;
                    label2.Text = success.ToString();
                    randomStart = false; //정답이면 난수가 초기화 되서 없어야 됨
                    return (cnt + "번 만에 정답(성공)!!");
                }

                fail++;
                label3.Text = fail.ToString();
                randomStart = false;
                return (cnt + "번 안에 성공하지 못했습니다.(실패) / 정답 : " + randomNum);
            }

            if (strikeNum.Equals(4))   //4 스트라이크일 경우 정답 반환
            {
                success++;
                label2.Text = success.ToString();
                randomStart = false; //정답이면 난수가 초기화 되서 없어야 됨
                return (cnt + "번 만에 정답(성공)!!");
            }
            return str;
        }

        private void Button1_Click(object sender, EventArgs e) //난수 생성(시작하기)
        {
            cnt = 0;

            Random r = new Random();

            while (true)
            {
                this.randomNum = r.Next(1000, 9999);

                //정답에 중복되는 숫자가 있는지 체크
                if (overlapCheck(this.randomNum.ToString()))  
                {
                    break;
                }
            }

            textBox1.AppendText("난수가 생성되었습니다! 수를 입력하세요");
            textBox1.Text += Environment.NewLine;

            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();

            randomStart = true; //난수가 생성되었음

            textBox2.Focus();
        }

        private void Button2_Click(object sender, EventArgs e) //정답 확인
        {
            MessageBox.Show(this.randomNum.ToString());
            textBox2.Focus();
        }

        private void Button3_Click(object sender, EventArgs e) //초기화
        {
            cnt = 0;

            textBox1.Clear();
            textBox2.Clear();
            textBox2.Focus();

            success = 0;
            fail = 0;
            label2.Text = success.ToString();
            label3.Text = fail.ToString();
        }

        private void Button4_Click(object sender, EventArgs e) //결과 확인
        {
            if (randomStart == false)
            {
                MessageBox.Show("난수를 먼저 생성해주세요");
                textBox2.Focus();
                return;
            }
            else if (textBox2.Text.Length != 4)
            {
                MessageBox.Show("4자리 숫자를 입력해주세요.");
                textBox2.Clear();
                textBox2.Focus();
                return;
            }
            else if (!overlapCheck(textBox2.Text))
            {
                MessageBox.Show("중복되는 숫자가 있습니다.");
                textBox2.Clear();
                textBox2.Focus();
                return;
            }
            else cnt++;

            String result = resultCheck(randomNum.ToString(), textBox2.Text);

            textBox1.AppendText("입력한 수 : " + textBox2.Text + " / 결과 : " + result + " (" + cnt + "번째)");
            textBox1.Text += Environment.NewLine;

            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();

            textBox2.Clear();
            textBox2.Focus();

        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (randomStart)
                MessageBox.Show("시작하기 전에 사용 가능합니다.");
            else
            {
                Form frm2 = new Form2(this);
                frm2.ShowDialog();
            }
        }
    }
}
