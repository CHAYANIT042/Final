using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Console_20211216
{
    class Program
    {

        #region Fields

        private static int _pointSkill = 0;
        private static List<Skill> _entityCollection = new List<Skill>();

        #endregion

        #region Main

        static void Main(string[] args)
        {
            AddCollection();
            ShowAllSkill();
            UpSkill();
        }

        #endregion

        #region Private Methods

        #region Update
        /// <summary>
        /// อัพสกิล
        /// </summary>
        private static void UpSkill()
        {
            string skillName = string.Empty;
            Skill entity = null;
            while (true)
            {
                Console.WriteLine("Input Skill Name :");
                skillName = Console.ReadLine();
                if (skillName == "?")
                {
                    ShowPointSkill();
                }
                else
                {
                    entity = _entityCollection.Find(x => x.SkillName.ToLower() == skillName.ToLower());
                    if (entity != null)
                    {
                        if (ValidateUpSkill(skillName) && ComfirmUpSkill(skillName))
                        {
                            entity.LvSkill += 1;
                        }
                    }
                }

            }
        }
                #endregion

        #region AddCollection
        /// <summary>
        /// สกิลทั้งหมด
        /// </summary>
        private static void AddCollection()
        {
            string[] skillArr = { "Fire Bolt", "Napalm Beat", "Soul Strike", "Safety Wall" };
            Skill entity = null;
            for (int i = 0; i < skillArr.Length; i++)
            {
                entity = new Skill();
                entity.SkillName = skillArr[i];
                _entityCollection.Add(entity);
            }
        }
         #endregion

        #region ComfirmUpSkill
        /// <summary>
        /// ยืนยันการอัพสกิล
        /// </summary>
        /// <param name="skillName"></param>
        /// <returns></returns>
        private static bool ComfirmUpSkill(string skillName)
        {
            string input = string.Empty;
            while (true)
            {
                Console.WriteLine("Add Dependency For {0} ? (Y/N)", skillName);
                input = Console.ReadLine();
                if (input.ToLower() == "y")
                {
                    return true;
                }
                else if (input.ToLower() == "n")
                {
                    return false;
                }
            }
        }
        #endregion

        #region ShowPointSkill
        /// <summary>
        /// แสดงค่าสกิลที่อัพไป
        /// </summary>
        private static void ShowPointSkill()
        {
            int pointSkill = _entityCollection.Sum(x => x.LvSkill);
            Console.WriteLine(pointSkill);
        }
        #endregion

        #region ShowAllSkill
        /// <summary>
        /// โชว์ชื่อสกิลทั้งหมดที่อัพได้
        /// </summary>
        private static void ShowAllSkill()
        {
            foreach (Skill entity in _entityCollection)
            {
                Console.WriteLine(entity.SkillName);
            }
        }
        #endregion

        #region ValidateUpSkill
        /// <summary>
        /// ตรวจสอบเงือนไขการอัพสกิล
        /// </summary>
        /// <param name="skillName"></param>
        /// <returns></returns>
        private static bool ValidateUpSkill(string skillName)
        {
            bool result = false;
            Skill entity = null;
            switch (skillName)
            {
                case "Fire Bolt":
                    result = true;
                    break;

                case "Napalm Beat":
                    result = true;
                    break;

                case "Soul Strike":
                case "Safety Wall":
                    entity = _entityCollection.Find(x => x.SkillName.ToLower() == "napalm beat");
                    // ต้องอัพสกิล napalm beat ก่อนถึงจะอัพ Soul Strike,Safety Wall ได้
                    if (entity != null && entity.LvSkill > 0)
                    {
                        result = true;
                    }
                    break;

                default:
                    break;
            }

            return result;
        }
        #endregion

        #endregion

    }
}
