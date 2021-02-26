using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models
{
  public class ChatMessage
  {
    public string SenderName { get; set; }

    public string Text { get; set; }

    public DateTimeOffset SentAt { get; set; }
  }
}
