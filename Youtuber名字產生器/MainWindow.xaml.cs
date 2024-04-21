using System;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace Youtuber名字產生器
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        string[] firstNameArray = ["有感","貓貓","筆電","菜菜","菜鳥","盜版","黑暗","滑鼠","培根","起源","陽光","殺手","電子","狗勾","狂暴", "緋紅", "SCP 愛科目三和有感", "狗狗喜歡有感","夜空"];
       string[] lastNameArray = ["滑鼠","電腦","神","培根","貓貓","菜鳥","狗勾","殺手","惡魔","粉絲", "皇家護衛隊", "筆電和珍珠貓的媽","夜空鑽石","鑽石", "筆電的粉絲! (正在嘗試10訂閱)", "而沒筆電","耳機"
           ,"健身","屁孩","玉米","平板","喵喵","小陳","香蕉","饅頭","programmer","桌機"];
        string[] randomTextArray = ["01", "0x","sa","sukia","nokia","undefined","null","404","error","nil","opengl","visual","code","warnings"];
        string[] specialNameArray = ["筆電皇家護衛隊"];
        string[] blockedName = ["菜鳥菜菜","盜版神","夜空夜空鑽石"];
        bool installed = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        private string randomFromArray(Array Array)
        {
            Random random = new Random();
            int lastNameInt = random.Next(0, Array.Length);
            string lastName = randomTextArray[lastNameInt];
            return lastName;
        }
        async private void regenerate()
        {
            Random random = new Random();
            int firstNameInt = random.Next(0, firstNameArray.Length);
            string firstName = firstNameArray[firstNameInt];
            GenName.Text = firstName + " ..";
            
            await Wait(500);
            int lastNameInt = random.Next(0, lastNameArray.Length);
            string lastName = lastNameArray[lastNameInt];
            for (int i = 0; i <  200; i++)
            {
                await Wait(10);
                GenName.Text = firstName + randomFromArray(randomTextArray);
            }
            
            GenName.Text = firstName + lastName;
            if(firstName ==  lastName || blockedName.Contains(GenName.Text))
            {
                GenName.Text = "唉呀出錯了!";
                await Wait(500);
                regenerate();
            }
            GenerateButton.IsEnabled = true;
            GenerateButton.Content = "再次生成";
        }
         
        async private void Button_Click(object sender, RoutedEventArgs e)
        {

            GenerateButton.IsEnabled = false;
            GenerateButton.Content = "正在下載艾坤大模型";
 
 
            LoadingCard.Visibility = Visibility.Visible;
            if (!installed)
            {
                var contentDialogService = new ContentDialogService();
                contentDialogService.SetContentPresenter(RootContentDialogPresenter);

                Task.Delay(1000);
                await contentDialogService.ShowSimpleDialogAsync(
                    new SimpleContentDialogCreateOptions()
                    {
                        Title = "將會使用114514GB的空間?",
                        Content = "安裝艾坤大模型將使用114514GB的空間. 請允許此動作",
                        PrimaryButtonText = "允許",
                        SecondaryButtonText = "不允許",
                        CloseButtonText = "取消"
                    }
                    );
                await Wait(1000);
                LoadingProgress.Value = 10;
                LoadingLabel.Content = "下載中 (1499GB/s)";
                await Wait(1000);
                LoadingProgress.Value = 50;
                LoadingLabel.Content = "下載中 (55553GB/s)";
                await Wait(100);
                LoadingLabel.Content = "解壓中...";
                await Wait(100);
                installed = true;

            }
            GenerateButton.Content = "已安裝";

            LoadingLabel.Content = "啟動模型中...";
            await Wait(100);
            LoadingLabel.Content = "python3 runmodel.py";
            LoadingProgress.Value = 80;
            await Wait(2000);
            LoadingLabel.Content = "生成完畢,清除快取中";
            LoadingProgress.Value = 100;
            // get random last name and first name
             regenerate();

        }

        private Task Wait(int Time)
        {
            return Task.Run( () =>
            {
                Thread.Sleep(Time);
            });
        }

    }
}