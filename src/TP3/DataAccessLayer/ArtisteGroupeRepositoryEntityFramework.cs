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
            List<Groupe> groupes = new List<Groupe>(); 
            ICollection<LienArtisteGroupe> liens = _context.LienArtisteGroupe.Where(x => x.IdArtiste == id).ToList();

            foreach (LienArtisteGroupe lien in liens)
            {
                groupes.Add(GetGroupe(lien.NomGroupe));
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
                else
                {
                    QuitterGroupe(artiste.IdArtiste, groupe.Nom);
                }
            }
            _context.Artistes.Remove(artiste);
            _context.SaveChanges();
        }

        public void UpdateArtiste(Artiste newArtiste)
        {
            Artiste originalArtiste = GetArtiste(newArtiste.IdArtiste);
            _context.Entry(originalArtiste).CurrentValues.SetValues(newArtiste);
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
            List<Artiste> artistes = new List<Artiste>();
            ICollection<LienArtisteGroupe> liens = _context.LienArtisteGroupe.Where(x => x.NomGroupe == nom).ToList();

            foreach (LienArtisteGroupe lien in liens)
            {
                artistes.Add(_context.Artistes.FirstOrDefault(x => x.IdArtiste == lien.IdArtiste));
            }

            return artistes;
        }

        public void DeleteGroupe(string nomGroupe)
        {
            Groupe groupe = GetGroupe(nomGroupe);
            foreach (LienArtisteGroupe lien in _context.LienArtisteGroupe.Where(x => x.NomGroupe == nomGroupe))
            {
                QuitterGroupe(lien.IdArtiste, lien.NomGroupe);
            }
      
            _context.Groupes.Remove(groupe);
            _context.SaveChanges();
        }

        public void UpdateGroupe(Groupe newGroupe)
        {
            Groupe originalGroupe = GetGroupe(newGroupe.Nom);
            _context.Entry(originalGroupe).CurrentValues.SetValues(newGroupe);
            _context.SaveChanges();
        }

        public void AddGroupe(Groupe groupe, int idArtiste, string role)
        {

            JoindreGroupe(idArtiste, groupe.Nom, role);

            _context.Groupes.Add(groupe);
            
            _context.SaveChanges();
        }

        //--------------------------LIENS ARTISTE GROUPE----------------------------------------------------------

        public void JoindreGroupe(int idArtiste, string nomGroupe, string role)
        {
            LienArtisteGroupe lien = new LienArtisteGroupe()
            {
                IdArtiste = idArtiste,
                NomGroupe = nomGroupe,
                RoleArtiste = role
            };

            _context.LienArtisteGroupe.Add(lien);

            _context.SaveChanges();
        }

        public void QuitterGroupe(int idArtiste, string nomGroupe)
        {
            LienArtisteGroupe lien = _context.LienArtisteGroupe.FirstOrDefault(
                x => x.IdArtiste == idArtiste && x.NomGroupe == nomGroupe
            );

            _context.LienArtisteGroupe.Remove(lien);

            _context.SaveChanges();
        }

    }
}