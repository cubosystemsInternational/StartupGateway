using System;
using Microsoft.Extensions.Logging;
using StartupGateway.DAL.Implementation;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Model;
using StartupGateway.UoW;
using StartupGateway.UoW.Interfaces;
using System.Collections.Generic;
using System.Linq;
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessLogic
{
    public class ScreensBLL
    {
        private readonly ILogger<ScreensBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to initialize ScreensBLL with necessary dependencies.
        /// </summary>
        public ScreensBLL(ILogger<ScreensBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the Screen information for the Id passed.
        /// </summary>
        /// <param name="screenId"></param>
        /// <returns>Screen?</returns>
        public Screen? GetScreenById(int screenId)
        {
            try
            {
                var screen = unitOfWork.GetDAL<IScreenDAL>().GetEntityById(screenId);
                if (screen != null)
                {
                    logger.LogInformation("Screen retrieved successfully at GetScreenById.");
                    return screen;
                }
                else
                {
                    logger.LogInformation("Screen retrieved at GetScreenById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetScreenById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the screens.
        /// </summary>
        /// <returns>List of Screen?</returns>
        public List<Screen>? GetAllScreens()
        {
            try
            {
                var listOfScreens = unitOfWork.GetDAL<IScreenDAL>().GetAllRecords().ToList();
                logger.LogInformation("Screens retrieved successfully at GetAllScreens.");
                return listOfScreens;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllScreens: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of Screen to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddScreen(Screen screen, int userId)
        {
            try
            {
                if (screen != null)
                {
                    screen.Status = EntityStatus.Active;
                    screen.ModifiedBy = userId;
                    screen.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IScreenDAL>().AddEntity(screen);
                    unitOfWork.Commit();
                    logger.LogInformation("Screen successfully added at AddScreen.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Screen is null at AddScreen");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddScreen: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of Screen. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newScreen"></param>
        /// <returns>True or False</returns>
        public bool UpdateScreen(Screen newScreen)
        {
            try
            {
                if (newScreen != null)
                {
                    Screen existingScreen = unitOfWork.GetDAL<IScreenDAL>().GetEntityById(newScreen.ScreenId);

                    // Update attributes if new values are not null or whitespace
                    existingScreen.ScreenName = !string.IsNullOrWhiteSpace(newScreen.ScreenName) ? newScreen.ScreenName : existingScreen.ScreenName;
                    existingScreen.Status = newScreen.Status != EntityStatus.Pending ? newScreen.Status : existingScreen.Status;
                    existingScreen.ModifiedOn = DateTime.Now;
                    existingScreen.ModifiedBy = newScreen.ModifiedBy != 0 ? newScreen.ModifiedBy : existingScreen.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("Screen updated successfully at UpdateScreen.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Screen passed at UpdateScreen is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateScreen: " + exception + ".");
                return false;
            }
        }
    }
}
