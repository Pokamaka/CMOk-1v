using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CMOk_1v
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_go_Click(object sender, RoutedEventArgs e)
        {
            list_result.Items.Clear();
            int n = 0;
            double t = 0; //время поступления клиенитов
            double timeService = 0;  //время обслуживания
            int sost = 10; //вероятность состояния
            try
            {
                n = int.Parse(tb_n.Text);
                t = double.Parse(tb_prixod.Text);
                timeService = double.Parse(tb_oneTime.Text);
            }
            catch
            {
                MessageBox.Show("Вы ввели не коректнве данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            double nu = 1 / timeService;
            double lamda = 1 / t;
            double alpha = lamda / nu;
            double p = 0;
            for (int k = 0; k < n; k++)
            {             
                p += (Math.Pow(alpha, k) / Factorial(k));                                            
            }

            double lop = Math.Pow(alpha, n) / (Factorial(n) * (1 - alpha / n));
            double P0 = 1 / (p + lop);
            double CHECK = 0;
            list_result.Items.Add("P0  " + Math.Round(P0, 3) + " - вероятность состоний многоканальной СМО");
            CHECK += P0;
            //list_result.Items.Add("---------------");
            double pN = 0;
            double l = 0;
            for (int k = 1; k <= sost ; k++)
            {
                if (k <= n)
                {
                    pN = P0 * (Math.Pow(alpha, k) / Factorial(k));
                }
                else
                {
                    pN = (P0 * Math.Pow(alpha, k)) / (Factorial(n) * Math.Pow(n, k - n));
                }
                CHECK += pN;
                list_result.Items.Add($"P{k}  "+ Math.Round(pN, 3) + $"- вероятность состояния многоканальной СМО");

                l += pN * ((alpha * n) / (Math.Pow(n - alpha, 2)));
            }
            //list_result.Items.Add("---------------");
            //list_result.Items.Add(Math.Round(l, 3) + "- средняя длина очереди");
            //list_result.Items.Add("---------------");
            //list_result.Items.Add($"{Math.Round(CHECK, 2)}");
        }

        static int Factorial(int x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                return x * Factorial(x - 1);
            }
        }
    }
}
 