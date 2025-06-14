﻿using HotelReservation.Exceptions;
using HotelReservation.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservation.Repository
{
    public class UserRepository : IUserRepository
    {
        private string ToCSV(User user)
        {
            return $"{user.Id},{user.Name},{user.Surname},{user.JMBG},{user.Username},{user.Password},{user.UserType},{user.IsActive} "; //sta sve upisuje u fajl
        }

        private User FromCSV(string csv)  //ovde cita iz fajla
        {
            string[] csvValues = csv.Split(',');

            var user = new User();
            user.Id = int.Parse(csvValues[0]);
            user.Name = csvValues[1];
            user.Surname = csvValues[2];
            user.JMBG = csvValues[3];
            user.Username = csvValues[4];
            user.Password = csvValues[5];
            user.UserType = csvValues[6];
            //try
            //{
            //    user.UserType = (UserType)Enum.Parse(typeof(UserType), csvValues[7], true);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error parsing UserType: {csvValues[7]}");
            //    Console.WriteLine(ex.Message);
            //    throw;
            //}

            user.IsActive = bool.Parse(csvValues[7]);

            return user;
        }

        public void Save(List<User> userList)
        {
            try
            {
                using (var streamWriter = new StreamWriter("users.txt"))
                {
                    foreach (var user in userList)
                    {
                        streamWriter.WriteLine(ToCSV(user));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CouldntPersistDataException(ex.Message);
            }

        }

        public List<User> Load()
        {
            if (!File.Exists("users.txt"))
            {
                return null;
            }

            try
            {
                using (var streamReader = new StreamReader("users.txt"))
                {
                    List<User> users = new List<User>();
                    string line;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var user = FromCSV(line);
                        users.Add(user);
                    }

                    return users;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new CouldntLoadResourceException(ex.Message);
            }
        }
    }
}
