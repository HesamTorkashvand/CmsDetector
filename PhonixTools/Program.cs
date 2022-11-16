using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace PhonixTools
{
    internal class Program
    {
        static void Main(string[] args)
        {

                string result;
                string payload;
                int Wordpresscount = 0;
                int joomlacount = 0;
                int nocmscount = 0;
                Console.Title = "Phonix Cms Detector";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("[+] Enter Your Domins List Path : ");
                string path = Console.ReadLine();
                string[] lines = System.IO.File.ReadAllLines(@path);
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine("[!] Scaning Start : ");
                foreach (string line in lines)
                {

                    // index wordpress check
                    string wppayload = "https://" + line + "/";
                    payload = wppayload;
                    var client = new RestClient(wppayload);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);
                    HttpStatusCode statusCode = response.StatusCode;
                    int numericStatusCode = (int)statusCode;
                    if (numericStatusCode == 200)
                    {
                        if (response.Content.Contains("wp-content"))
                        {
                            result = "Wordpress { Index Found }";
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("[+] " + payload + " => " + result + "=> status code => " + numericStatusCode.ToString());
                            Wordpresscount++;
                            Console.Title = "Phonix Cms Detector        =>      Wordpress :  " + Wordpresscount.ToString() + "     |     joomla  :  " + joomlacount.ToString() + "     |      NoCms :  " + nocmscount.ToString();
                            if (!File.Exists("wordpress_sites.txt"))
                            {
                                File.Create("wordpress_sites.txt");
                            }
                            File.AppendAllText("wordpress_sites.txt", wppayload + Environment.NewLine);

                        }
                        else
                        {
                            //wordpress blog check
                            string payload3 = "https://" + line + "/blog";
                            var client3 = new RestClient(payload3);
                            client3.Timeout = -1;
                            var request3 = new RestRequest(Method.GET);
                            IRestResponse response3 = client3.Execute(request3);
                            HttpStatusCode statusCode3 = response3.StatusCode;
                            int numericStatusCode3 = (int)statusCode3;
                            if (numericStatusCode3 == 200)
                            {
                                if (response3.Content.Contains("wp-content"))
                                {
                                    result = "Wordpress { Blog Found }";
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("[+] " + payload3 + " => " + result + "=> status code => " + numericStatusCode3.ToString());
                                    Wordpresscount++;
                                    Console.Title = "Phonix Cms Detector        =>      Wordpress :  " + Wordpresscount.ToString() + "     |     joomla  :  " + joomlacount.ToString() + "     |      NoCms :  " + nocmscount.ToString();
                                    if (!File.Exists("wordpress_sites.txt"))
                                    {
                                        File.Create("wordpress_sites.txt");
                                    }
                                    File.AppendAllText("wordpress_sites.txt", payload3 + Environment.NewLine);
                                }
                                else
                                {
                                    //xmlrpc wordpress check
                                    string payload2 = "https://" + line + "/xmlrpc.php";
                                    var client2 = new RestClient(payload2);
                                    client2.Timeout = -1;
                                    var request2 = new RestRequest(Method.GET);
                                    IRestResponse response2 = client2.Execute(request2);
                                    HttpStatusCode statusCode2 = response2.StatusCode;
                                    int numericStatusCode2 = (int)statusCode2;
                                    if (numericStatusCode2 == 403 || numericStatusCode2 == 405)
                                    {
                                        result = "Wordpress { Xmlrpc Found }";
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("[+] " + payload + " => " + result + "=> status code => " + numericStatusCode3.ToString());
                                        Wordpresscount++;
                                        Console.Title = "Phonix Cms Detector        =>      Wordpress :  " + Wordpresscount.ToString() + "     |     joomla  :  " + joomlacount.ToString() + "     |      NoCms :  " + nocmscount.ToString();
                                        if (!File.Exists("wordpress_sites.txt"))
                                        {
                                            File.Create("wordpress_sites.txt");
                                        }
                                        File.AppendAllText("wordpress_sites.txt", payload + Environment.NewLine);
                                    }
                                    else if (numericStatusCode2 == 200)
                                    {
                                        if (response2.Content.ToString().Contains("XML-RPC server accepts POST requests only."))
                                        {
                                            result = "Wordpress { Xmlrpc Found }";
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.WriteLine("[+] " + payload + " => " + result + "=> status code => " + numericStatusCode2.ToString());
                                            Wordpresscount++;
                                            Console.Title = "Phonix Cms Detector        =>      Wordpress :  " + Wordpresscount.ToString() + "     |     joomla  :  " + joomlacount.ToString() + "     |      NoCms :  " + nocmscount.ToString();
                                            if (!File.Exists("wordpress_sites.txt"))
                                            {
                                                File.Create("wordpress_sites.txt");
                                            }
                                            File.AppendAllText("wordpress_sites.txt", payload + Environment.NewLine);

                                        }
                                        else
                                        {
                                            string payload4 = "https://" + line + "/administrator";
                                            var client4 = new RestClient(payload4);
                                            client4.Timeout = -1;
                                            var request4 = new RestRequest(Method.GET);
                                            IRestResponse response4 = client4.Execute(request4);
                                            HttpStatusCode statusCode4 = response4.StatusCode;
                                            int numericStatusCode4 = (int)statusCode4;
                                            if (numericStatusCode4 == 200)
                                            {
                                                if (response4.Content.Contains("joomla"))
                                                {
                                                    result = "Joomla { Login Found }";
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine("[+] " + payload + " => " + result + "=> status code => " + numericStatusCode4.ToString());
                                                    joomlacount++;
                                                    Console.Title = "Phonix Cms Detector        =>      Wordpress :  " + Wordpresscount.ToString() + "     |     joomla  :  " + joomlacount.ToString() + "     |      NoCms :  " + nocmscount.ToString();
                                                    if (!File.Exists("joomla_sites.txt"))
                                                    {
                                                        File.Create("joomla_sites.txt");
                                                    }
                                                    if (!File.Exists("wordpress_sites.txt"))
                                                    {
                                                        File.Create("wordpress_sites.txt");
                                                    }
                                                    File.AppendAllText("joomla_sites.txt", payload + Environment.NewLine);

                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("[-] " + payload + " => Dont have cms or service is offline ! => status code => " + numericStatusCode.ToString());
                                                    nocmscount++;
                                                    Console.Title = "Phonix Cms Detector        =>      Wordpress :  " + Wordpresscount.ToString() + "     |     joomla  :  " + joomlacount.ToString() + "     |      NoCms :  " + nocmscount.ToString();

                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine("[-] " + payload + " => Dont have cms or service is offline ! => status code => " + numericStatusCode.ToString());
                                        nocmscount++;
                                        Console.Title = "Phonix Cms Detector        =>      Wordpress :  " + Wordpresscount.ToString() + "     |     joomla  :  " + joomlacount.ToString() + "     |      NoCms :  " + nocmscount.ToString();

                                    }
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("[-] " + payload + " => Dont have cms or service is offline ! => status code => " + numericStatusCode.ToString());
                                nocmscount++;
                                Console.Title = "Phonix Cms Detector        =>      Wordpress :  " + Wordpresscount.ToString() + "     |     joomla  :  " + joomlacount.ToString() + "     |      NoCms :  " + nocmscount.ToString();

                            }

                        }

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[!] " + payload + " =>  Service is offline ! | status code => " + numericStatusCode.ToString());
                        nocmscount++;
                        Console.Title = "Phonix Cms Detector        =>      Wordpress :  " + Wordpresscount.ToString() + "     |     joomla  :  " + joomlacount.ToString() + "     |      NoCms :  " + nocmscount.ToString();

                    }
                }
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
