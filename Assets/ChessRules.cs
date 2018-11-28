using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
public class ChessRules : MonoBehaviour 
{
    public InputField complaints;
   public void loadURL()
    {

        Application.OpenURL("http://www.chesscoachonline.com/chess-articles/chess-rules");


    }
   
   public void sendEmail()
    {
        try
        {MailMessage mail = new MailMessage();
        mail.From = new MailAddress("Joutchkov355@gmail.com");
        mail.To.Add("Joutchkov.training@gmail.com");
        mail.Subject = "Chess Bug";
        mail.Body = complaints.text;

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("Joutchkov335@gmail.com", "%K87fam7") as ICredentialsByHost;
        smtpServer.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };
            smtpServer.Send(mail);
            Debug.Log("success");
        }
        catch(SmtpException e){
            Debug.Log("Your complaint has been shredded, thanks for playing :)");
        }

    }
}

