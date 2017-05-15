﻿using System;
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

        public void DeleteArtiste(int id)
        {
            Artiste artiste = GetArtiste(id);
            foreach (LienArtisteGroupe lien in _context.LienArtisteGroupe.Where(x => x.IdArtiste == id))
            {
                if (GetGroupe(lien.NomGroupe).Artistes.Count == 1)
                {
                    DeleteGroupe(lien.NomGroupe);
                }
                _context.LienArtisteGroupe.Remove(lien);
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

        public void DeleteGroupe(string nomGroupe)
        {
            Groupe groupe = GetGroupe(nomGroupe);
            foreach (LienArtisteGroupe lien in _context.LienArtisteGroupe.Where(x => x.NomGroupe == nomGroupe))
            {
                Artiste artiste = GetArtiste(lien.IdArtiste);
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

        public void AddGroupe(Groupe groupe)
        {
            _context.Groupes.Add(groupe);
            _context.SaveChanges();
        }
    }
}