using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teilnehmerverwaltung.Models;

public class Umschulung
{
    public string KursNr { get; set; }
    public string Bezeichnung { get; set; }
    public DateOnly Beginn { get; set; }
    public DateOnly Ende { get; set; }
    public string Raum { get; set; }
}
