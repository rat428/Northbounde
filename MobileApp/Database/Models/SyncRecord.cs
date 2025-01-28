using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northboundei.Mobile.Database.Models;

public class SyncRecord
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public DateTime SyncDate { get; set; }
}
