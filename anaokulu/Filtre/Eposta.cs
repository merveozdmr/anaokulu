using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace anaokulu.Filtre
{
    public class Eposta
    {
        public static bool Gonder(string gidecekEposta, string konu, string mesaj)

        {
            try
            {
                MailMessage eposta = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                string gonderenEposta = "merveozdemir.9393@gmailcom";
                string gonderenSifre = "omerve321";

                smtp.Credentials = new System.Net.NetworkCredential(gonderenEposta, gonderenSifre);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;

                eposta.IsBodyHtml = true;
                eposta.From = new MailAddress(gonderenEposta);
                eposta.To.Add(gidecekEposta);
                eposta.Subject = konu;
                eposta.Body = mesaj;


                smtp.Send(eposta);


                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}