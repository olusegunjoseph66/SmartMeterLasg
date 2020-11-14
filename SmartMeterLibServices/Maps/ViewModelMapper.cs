using SmartMeterLibServices.Model;
using SmartMeterLibServices.ViewModel;
using System;

namespace SmartMeterLibServices.Maps
{
    public static class ViewModelMapper
    {


        public static SubscriberInfoModel Map(this SubscriberCreateEditViewModel model)
        {
            if (model == null) return null;

            return new SubscriberInfoModel
            {
                Gender = model.Gender,
                Date_Of_Registration = model.Date_Of_Registration,
                Dofb = model.Dofb.ToString(),
                STAKEHOLDER_ID = model.STAKEHOLDER_ID,
                Email = model.Email,
                Address = model.Address,
                Country = model.Country,
                Subscriber_ID = model.Subscriber_ID,
                First_Name = model.First_Name,
                Last_Name = model.Last_Name,
                Phone = model.Phone,
                State = model.State,
                UserId = model.UserId

            };
        }


        public static SubscriberCreateEditViewModel Map(this SubscriberInfoModel model)
        {
            if (model == null) return null;

            return new SubscriberCreateEditViewModel
            {
                Gender = model.Gender,
                Date_Of_Registration = model.Date_Of_Registration,
                Dofb = Convert.ToDateTime(model.Dofb),
                STAKEHOLDER_ID = model.STAKEHOLDER_ID,
                Email = model.Email,
                Address = model.Address,
                Country = model.Country,
                Subscriber_ID = model.Subscriber_ID,
                First_Name = model.First_Name,
                Last_Name = model.Last_Name,
                Phone = model.Phone,
                State = model.State,
                UserId = model.UserId
            };
        }






        public static StackholderInfoModel Map(this StackholderCreateViewModel model)
        {
            if (model == null) return null;

            return new StackholderInfoModel
            {

                Date_Of_Registration = model.Date_Of_Registration,
                Stackholder_ID = model.Stackholder_ID,
                Email = model.Email,
                Address = model.Address,
                Country = model.Country,
                Sackholder_Name = model.Sackholder_Name,
                UserId = model.UserId

            };
        }


        public static StackholderCreateViewModel Map(this StackholderInfoModel model)
        {
            if (model == null) return null;
            return new StackholderCreateViewModel
            {

                Date_Of_Registration = model.Date_Of_Registration,
                Stackholder_ID = model.Stackholder_ID,
                Email = model.Email,
                Address = model.Address,
                Country = model.Country,
                Sackholder_Name = model.Sackholder_Name,
                UserId = model.UserId

            };
        }
    }
}
