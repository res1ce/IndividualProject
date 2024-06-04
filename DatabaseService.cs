using IndidProject.models;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace IndidProject
{
    public static class DatabaseService
    {
        static public User GetUser(string login, string pass_1)
        {
            using (AppDbContext db = new AppDbContext())
            {
                return db.Users.Where(b => b.login == login && b.password == pass_1).FirstOrDefault();
            }
        }

        static public ObservableCollection<Apteka> GetAptekas()
        {
            using (AppDbContext db = new AppDbContext())
            {
                return new ObservableCollection<Apteka>(db.Aptekas.ToList());
            }
        }

        static public void AddApteka(Apteka apteka)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Aptekas.Add(apteka);
                db.SaveChanges();
            }
        }          

        static public void DeleteApteka(int number)
        {
            using (var context = new AppDbContext())
            {
                var apteka = context.Aptekas.SingleOrDefault(a => a.number == number);
                if (apteka != null)
                {
                    context.Aptekas.Remove(apteka);
                    context.SaveChanges();
                }
            }
        }    
        
        static public void UpdateApteka(Apteka editedApteka)
        {
            using (var context = new AppDbContext())
            {
                var apteka = context.Aptekas.SingleOrDefault(a => a.number == editedApteka.number);
                if (apteka != null)
                {
                    apteka.name = editedApteka.name;
                    apteka.address = editedApteka.address;
                    apteka.start_work_time = editedApteka.start_work_time;
                    apteka.end_work_time = editedApteka.end_work_time;
                    context.SaveChanges();
                }
            }
        }
        
        static public List<DrugGroup> GetDrugGroups()
        {
            using (var context = new AppDbContext())
            {
                return context.DrugGroups.ToList();
            }
        }

        static public void AddDrug(Drug drug)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Drugs.Add(drug);
                db.SaveChanges();
            }
        }

        static public ObservableCollection<Drug> GetDrugs()
        {
            using (AppDbContext db = new AppDbContext())
            {
                return new ObservableCollection<Drug>(db.Drugs.ToList());
            }
        }

        static public void UpdateDrugs(Drug editedDrug)
        {
            using (var context = new AppDbContext())
            {
                var drug = context.Drugs.SingleOrDefault(a => a.number == editedDrug.number);
                if (drug != null)
                {
                    drug.name = editedDrug.name;
                    drug.groupId = editedDrug.groupId;
                    drug.dosage = editedDrug.dosage;
                    drug.expiration_days = editedDrug.expiration_days;
                    context.SaveChanges();
                }
            }
        }

        static public void DeleteDrug(int number)
        {
            using (var context = new AppDbContext())
            {
                var drug = context.Drugs.SingleOrDefault(a => a.number == number);
                if (drug != null)
                {
                    context.Drugs.Remove(drug);
                    context.SaveChanges();
                }
            }
        }
        
        static public string GetGroupNameByGroupId(int groupId)
        {
            using (var context = new AppDbContext())
            {
                return context.DrugGroups.Where(b => b.id == groupId).FirstOrDefault().name;
            }
        }

        static public void AddAvaibality(Avaibality avaible)
        {
            using (AppDbContext db = new AppDbContext())
            {
                db.Availabilitys.Add(avaible);
                db.SaveChanges();
            }
        }

        static public ObservableCollection<Avaibality> GetAvaibality()
        {
            using (AppDbContext db = new AppDbContext())
            {
                return new ObservableCollection<Avaibality>(db.Availabilitys.ToList());
            }
        }

        static public void DeleteAvaibality(int number)
        {
            using (var context = new AppDbContext())
            {
                var avaibality = context.Availabilitys.SingleOrDefault(a => a.id == number);
                if (avaibality != null)
                {
                    context.Availabilitys.Remove(avaibality);
                    context.SaveChanges();
                }
            }
        }

        static public void UpdateAvaibality(Avaibality editedAvaibality)
        {
            using (var context = new AppDbContext())
            {
                var avaibality = context.Availabilitys.SingleOrDefault(a => a.id == editedAvaibality.id);
                if (avaibality != null)
                {
                    avaibality.number_apteka = editedAvaibality.number_apteka;
                    avaibality.number_drug = editedAvaibality.number_drug;
                    avaibality.release_date = editedAvaibality.release_date;
                    avaibality.quantity = editedAvaibality.quantity;
                    avaibality.cost = editedAvaibality.cost;
                    context.SaveChanges();
                }
            }
        }
    }
}
