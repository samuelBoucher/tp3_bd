using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3.DataAccessLayer.Interface;
using TP3.Entities;

namespace TP3.DataAccessLayer
{
    public class ArtisteGroupeRepositoryEntityFramework : IArtisteGroupeRepository
    {
        private readonly HedgesProductionsContext _context;

        public ArtisteGroupeRepositoryEntityFramework(HedgesProductionsContext dbContext)
        {
            _context = dbContext;
        }

        //--------------------------ARTISTE----------------------------------------------------------

        public Artiste GetArtiste(int id)
        {
            return _context.Artistes.FirstOrDefault(x => x.IdArtiste == id);
        }

        public IEnumerable<Groupe> GetGroupesForArtiste(int id)
        {
            IEnumerable<Groupe> groupes = new List<Groupe>(); 
            ICollection<LienArtisteGroupe> liens = _context.LienArtisteGroupe.Where(x => x.IdArtiste == id).ToList();

            foreach (LienArtisteGroupe lien in liens)
            {
                groupes.Append(GetGroupe(lien.NomGroupe));
            }

            return groupes;
        }

        public void DeleteArtiste(int id)
        {
            Artiste artiste = GetArtiste(id);
            foreach (Groupe groupe in GetGroupesForArtiste(id))
            {
                if (GetArtistesForGroupe(groupe.Nom).Count() == 1)
                {
                    DeleteGroupe(groupe.Nom);
                }
                _context.LienArtisteGroupe.Remove(_context.LienArtisteGroupe.FirstOrDefault(x => x.IdArtiste == id));
            }
            _context.Artistes.Remove(artiste);
            _context.SaveChanges();
        }

        public void UpdateArtiste(Artiste newArtiste)
        {
            _context.Artistes.Update(newArtiste);
            _context.SaveChanges();
        }

        public void AddArtiste(Artiste artiste)
        {
            _context.Artistes.Add(artiste);
            _context.SaveChanges();
        }

        //--------------------------GROUPE----------------------------------------------------------

        public Groupe GetGroupe(string nomGroupe)
        {
            return _context.Groupes.FirstOrDefault(x => x.Nom == nomGroupe);
        }

        public IEnumerable<Artiste> GetArtistesForGroupe(string nom)
        {
            IEnumerable<Artiste> artistes = new List<Artiste>();
            ICollection<LienArtisteGroupe> liens = _context.LienArtisteGroupe.Where(x => x.NomGroupe == nom).ToList();

            foreach (LienArtisteGroupe lien in liens)
            {
                artistes.Append(_context.Artistes.FirstOrDefault(x => x.IdArtiste == lien.IdArtiste));
            }

            return artistes;
        }

        public void DeleteGroupe(string nomGroupe)
        {
            Groupe groupe = GetGroupe(nomGroupe);
            foreach (LienArtisteGroupe lien in _context.LienArtisteGroupe.Where(x => x.NomGroupe == nomGroupe))
            {
                _context.LienArtisteGroupe.Remove(lien);
            }
            _context.Groupes.Remove(groupe);
            _context.SaveChanges();
        }

        public void UpdateGroupe(Groupe groupe)
        {
            _context.Groupes.Update(groupe);
            _context.SaveChanges();
        }

        public void AddGroupe(Groupe groupe, int idArtiste, string role)
        {
            Artiste artiste = GetArtiste(idArtiste); 

            LienArtisteGroupe lien = new LienArtisteGroupe()
            {
                IdArtiste = idArtiste,
                NomGroupe = groupe.Nom,
                RoleArtiste = role
            };

            _context.LienArtisteGroupe.Add(lien);
            _context.Groupes.Add(groupe);
            
            _context.SaveChanges();
        }
    }
}