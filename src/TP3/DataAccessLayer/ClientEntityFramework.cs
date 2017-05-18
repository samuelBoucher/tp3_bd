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
            var originalClient = _context.Clients.Find(updatedClient.CodeClient);

            if (originalClient != null)
            {
                _context.Entry(originalClient).CurrentValues.SetValues(updatedClient);
                _context.SaveChanges();
            }
        }

        public void DeleteClient(Client client)
        {
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }

        public void AddContrat(Contrat contrat)
        {
            _context.Contrats.Add(contrat);
            _context.SaveChanges();
        }

        public void UpdateContrat(Contrat updatedContrat)
        {
            var originalContrat = _context.Contrats.Find(updatedContrat.NoContrat);

            if (originalContrat != null)
            {
                _context.Entry(originalContrat).CurrentValues.SetValues(updatedContrat);
                _context.SaveChanges();
            }
        }

        public void DeleteContrat(Contrat contrat)
        {
            _context.Contrats.Remove(contrat);
            _context.SaveChanges();
        }

        public void AddFacture(Facture facture)
        {
            _context.Factures.Add(facture);
            _context.SaveChanges();
        }

        public void UpdateFacture(Facture updatedFacture)
        {
            var originalClient = _context.Factures.Find(updatedFacture.NoFacture);

            if (originalClient != null)
            {
                _context.Entry(originalClient).CurrentValues.SetValues(updatedFacture);
                _context.SaveChanges();
            }
        }

        public void DeleteFacture(Facture facture)
        {
            _context.Factures.Remove(facture);
            _context.SaveChanges();
        }
    }
}