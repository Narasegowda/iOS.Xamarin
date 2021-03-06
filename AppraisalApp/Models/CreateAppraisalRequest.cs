﻿using System;
namespace ExtAppraisalApp.Models
{
    public class CreateAppraisalRequest
    {
        private short _storeID;
        public string VIN { get; set; }
        public int Mileage { get; set; }
        public string DDCUserId { get; set; }
        public short? StoreID
        {
            get
            {
                return _storeID==0?(short)2001:(short)_storeID;
            }
            set
            {
                _storeID = (value == null|| value == 0)? (short)2001 : (short)value;
            }
 
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Is_Extrn_Customer { get; set; }
 
    }
 
}
