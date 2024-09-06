using System;
using System.Collections.Generic;
using System.Linq;

namespace DBHelpClass.DBHelper
{
    public class OperationHelper
    {
        public VisitAppEntities VisitAppEntities = new VisitAppEntities();

        public List<T_PersonBasic> GetPersonBasicsByRoom(string workRoom)
        {
            return VisitAppEntities.T_PersonBasic.Where(x => x.WorkRoom == workRoom).ToList();
        }

        public void UpdatePerSonBasic(List<T_PersonBasic> t_PersonBasics)
        {
            foreach (var item in t_PersonBasics)
            {
                var update = VisitAppEntities.T_PersonBasic.FirstOrDefault(x => x.Id == item.Id);
                if (update == null)
                {
                    VisitAppEntities.T_PersonBasic.Add(item);
                }
                else
                {
                    update.PsnName = item.PsnName;
                    update.PsnPic = item.PsnPic;
                    update.Contact = item.Contact;
                    update.WorkRoom = item.WorkRoom;
                }
                VisitAppEntities.SaveChanges();

            }
        }
        public void DeletePerSonBasic(int id)
        {
            var update = VisitAppEntities.T_PersonBasic.FirstOrDefault(x => x.Id == id);
            if (update != null)
            {
                VisitAppEntities.T_PersonBasic.Remove(update);
                VisitAppEntities.SaveChanges();
            }

        }
        /// <summary>
        /// 获取人员表新ID
        /// </summary>
        /// <returns></returns>
        public int GetT_PersonNewId()
        {
            if (!VisitAppEntities.T_PersonBasic.Any())
            {
                return 1;
            }
            return VisitAppEntities.T_PersonBasic.Max(x => x.Id) + 1;
        }

        /// <summary>
        /// 外来施工单位Id
        /// </summary>
        /// <returns></returns>
        public int GetT_OuterWorkderNewId()
        {
            if (!VisitAppEntities.T_OuterWorkder.Any())
            {
                return 1;
            }
            return VisitAppEntities.T_OuterWorkder.Max(x => x.Id) + 1;
        }

        public List<T_OuterWorkder> GetOuterWorkdersByDate(string workRoom, DateTime date)
        {
            return VisitAppEntities.T_OuterWorkder.Where(x => x.WorkRoom == workRoom && x.StartTime <= date && x.EndTime > date).ToList();
        }

        public List<T_OuterWorkder> GetOuterWorkdersByRoom(string workRoom)
        {
            return VisitAppEntities.T_OuterWorkder.Where(x => x.WorkRoom == workRoom && x.EndTime > DateTime.Now).ToList();
        }


        public void UpdateOuterWorker(List<T_OuterWorkder> t_OuterWorkders)
        {
            foreach (var item in t_OuterWorkders)
            {
                var update = VisitAppEntities.T_OuterWorkder.FirstOrDefault(x => x.Id == item.Id);
                if (update == null)
                {
                    VisitAppEntities.T_OuterWorkder.Add(item);
                }
                else
                {
                    update.Id = item.Id;
                    update.Company = item.Company;
                    update.ResponsePsn = item.ResponsePsn;
                    update.Contact = item.Contact;
                    update.Count = item.Count;
                    update.WorkContent = item.WorkContent;
                    update.StartTime = item.StartTime;
                    update.EndTime = item.EndTime;
                    update.WorkRoom = item.WorkRoom;
                }
                VisitAppEntities.SaveChanges();

            }
        }


        public void UpdateT_ClassSets(List<T_ClassSet> t_ClassSets)
        {
            foreach (var item in t_ClassSets)
            {
                var update = VisitAppEntities.T_ClassSet.FirstOrDefault(x => x.PsnName == item.PsnName && x.ClassDate == item.ClassDate.Date);
                if (update == null)
                {
                    VisitAppEntities.T_ClassSet.Add(item);
                }
                else
                {
                    update.Id = item.Id;
                    update.PsnName = item.PsnName;
                    update.ClassDate = item.ClassDate;
                    update.ClassType = item.ClassType;
                    update.ClassMaster = item.ClassMaster;
                    update.WorkRoom = item.WorkRoom;
                    update.WorkRoom = item.WorkRoom;
                }
                VisitAppEntities.SaveChanges();
            }
        }


        public List<T_ClassSet> GetT_ClassSetsByRoom(string roomName, DateTime startDate, DateTime endDate)
        {

            return VisitAppEntities.T_ClassSet.Where(x => x.WorkRoom == roomName && x.ClassDate >= startDate && x.ClassDate < endDate).ToList();

        }
        public T_PersonBasic GetT_PersonBasicByPsnName(string roomName, string psnName)
        {
            return VisitAppEntities.T_PersonBasic.FirstOrDefault(x => x.WorkRoom == roomName && x.PsnName == psnName);
        }
    }
}
