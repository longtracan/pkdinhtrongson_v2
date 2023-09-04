using Entities.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using TLBD.Models.EntityModel;

namespace TLBD.Models
{
    public class Comment_Class
    {
        EntitiesData ctx = new EntitiesData();

        public void Add(Comment_Model model)
        {
            COMMENT cmt = new COMMENT()
            {
                ID=model.ID,
                Name=model.Name,
                ProductID=model.ProductID,
                Comment1=model.Comment,
                Date=model.Date
            };

            ctx.COMMENTs.Add(cmt);
            try
            {
                ctx.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }
    }
}