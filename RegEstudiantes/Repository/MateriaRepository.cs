using Dapper;
using RegEstudiantes.DataContext;
using RegEstudiantes.Interfaces;
using RegEstudiantes.Models;
using System.Data;

namespace RegEstudiantes.Repository
{
    public class MateriaRepository : IMateriaRepository
    {
        private readonly DapperContext _context;
        public MateriaRepository(DapperContext context)
        {
            _context = context;
        }            

        public async Task<Materia> GetMateriaId(int id)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                string sql = @"SELECT s.*, p.* 
                               FROM Materias s
                               INNER JOIN Profesores p ON s.ProfesorId = p.Id
                               WHERE s.Id = @Id";

                var result = await db.QueryAsync<Materia, Profesor, Materia>(
                    sql,
                    (materia, profesor) => {
                        materia.profesor = profesor;
                        return materia;
                    },
                    new { Id = id },
                    splitOn: "Id"
                );

                return result.FirstOrDefault();
            }
            finally
            {
                if(db.State == ConnectionState.Open)
                   db.Close();
            }            
        }

        public async Task<List<Materia>> GetMaterias()
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                string sql = @"SELECT s.*, p.* 
                               FROM Materias s
                               INNER JOIN Profesores p ON s.ProfesorId = p.Id";                

                var result = await db.QueryAsync<Materia, Profesor, Materia>(
                    sql,
                    (materia, profesor) => {
                        materia.profesor = profesor;
                        return materia;
                    },
                    splitOn: "Id"
                );

                return result.ToList();
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }            
        }

        public async Task<List<Materia>> GetListIds(List<int> ids)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                string sql = @"SELECT s.*, p.* 
                               FROM Materias s
                               INNER JOIN Profesores p ON s.ProfesorId = p.Id
                               WHERE s.Id IN @Ids";                

                var result = await db.QueryAsync<Materia, Profesor, Materia>(
                    sql,
                    (materia, profesor) => {
                        materia.profesor = profesor;
                        return materia;
                    },
                    new { Ids = ids },
                    splitOn: "Id"
                );

                return result.ToList();
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }            
        }

        public async Task<List<Materia>> GetProfesorId(int professorId)
        {
            using IDbConnection db = _context.CreateConnection();
            try
            {
                string sql = @"SELECT s.*, p.* 
                               FROM Materias s
                               INNER JOIN Profesores p ON s.ProfesorId = p.Id
                               WHERE s.ProfesorId = @ProfesorId";                

                var result = await db.QueryAsync<Materia, Profesor, Materia>(
                    sql,
                    (materia, profesor) => {
                        materia.profesor = profesor;
                        return materia;
                    },
                    new { ProfesorId = professorId },
                    splitOn: "Id"
                );

                return result.ToList();
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }            
        }
    }
}
