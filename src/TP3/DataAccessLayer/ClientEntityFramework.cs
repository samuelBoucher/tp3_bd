﻿using System;
using TP3.Entities;

namespace TP3.DataAccessLayer
{
    public class ClientEntityFramework
    {
        private readonly HedgesProductionsContext _context;

        public ClientEntityFramework(HedgesProductionsContext context)
        {
            _context = context;
        }

        public void AddClient(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void UpdateClient(Client updatedClient)
        {
            _context.Clients.Attach(updatedClient);
            //_context.Entry(updatedClient).State = System.Data.EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
