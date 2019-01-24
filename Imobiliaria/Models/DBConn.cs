using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Imobiliaria.FluentApi;
using System.Collections.Generic;
using System;

namespace Imobiliaria.Models
{
    public class DBConn : DbContext
    {
        public DBConn(): base("DBContexto") {
          //  Database.SetInitializer(new InicialiarDB());

        }

        public DbSet<Cidade> Cidades { get; set; } 
        public DbSet<Estado> Estados { get; set; }
        public DbSet<TipoImovel> TipoImoveis { get; set; }
        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<ImagensAnuncio> Imagens { get; set; }
        public DbSet<Bairro> Bairros { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnuncioFluentApi());
            modelBuilder.Configurations.Add(new ImagensFluentApi());
            modelBuilder.Configurations.Add(new TipoImovelFluentApi());
            modelBuilder.Configurations.Add(new EstadoFluentApi());
            modelBuilder.Configurations.Add(new CidadeFluentApi());
            modelBuilder.Configurations.Add(new BairroFluentApi());
            modelBuilder.Configurations.Add(new EnderecoFluentApi());
            modelBuilder.Configurations.Add(new UsuarioFluentApi());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }

    public class InicialiarDB : DropCreateDatabaseIfModelChanges<DBConn>
    {
        protected override void Seed(DBConn context)
        {

            new List<Estado>() {
                new Estado()
                {
                     Nome = "São Paulo",
                     Sigla = "SP",
                     Ativo = true,
                     CidadesUF = new List<Cidade>() {
                                    new Cidade()
                                    {
                                        Nome = "São Paulo",
                                        EstadoId = 1,
                                        Ativo = true,
                                        BairrosCidades = new List<Bairro>() {
                                             new Bairro() {
                                                Nome = "Penha",
                                                Ativo = true,
                                                CidadeId = 1
                                            },
                                            new Bairro() {
                                                Nome = "Tatuape",
                                                Ativo = true,
                                                CidadeId = 1
                                            },
                                        }
                                    },
                                     new Cidade()
                                    {
                                        Nome = "Poa",
                                        EstadoId = 1,
                                        Ativo = true,
                                        BairrosCidades =  new List<Bairro>() {
                                                 new Bairro() {
                                                Nome = "Jardim América",
                                                Ativo = true,
                                                CidadeId = 2
                                            },
                                            new Bairro() {
                                                Nome = "Dom Manoel",
                                                Ativo = true,
                                                CidadeId = 2
                                            },
                                        }
                                    },
                     }
                },
                 new Estado()
                {
                     Nome = "Rio de Janeiro",
                     Sigla = "RJ",
                     Ativo = true,
                     CidadesUF = new List<Cidade>() {
                                  new Cidade()
                                    {
                                        Nome = "Duque de Caxias",
                                        EstadoId = 2,
                                        Ativo = true,
                                        BairrosCidades = new List<Bairro>() {
                                                new Bairro() {
                                                Nome = "Campos Elíseos",
                                                Ativo = true,
                                                CidadeId = 3
                                            },

                                               new Bairro() {
                                                Nome = "Centro",
                                                Ativo = true,
                                                CidadeId = 3
                                            },
                                        }
                                    },
                                      new Cidade()
                                    {
                                        Nome = "Niteroi",
                                        EstadoId = 2,
                                        Ativo = true,
                                        BairrosCidades =  new List<Bairro>()
                                        {
                                            new Bairro() {
                                                Nome = "Ilha da Conceição",
                                                Ativo = true,
                                                CidadeId = 4
                                            },
                                               new Bairro() {
                                                Nome = "Itaipu",
                                                Ativo = true,
                                                CidadeId = 4
                                            },
                                        }
                                    },
                     }
                },
            new Estado()
            {
                Nome = "Minas Gerais",
                Sigla = "MG",
                Ativo = true,
                CidadesUF = new List<Cidade>() {
                                     new Cidade()
                                    {
                                        Nome = "Belo Horizonte",
                                        EstadoId = 3,
                                        Ativo = true,
                                        BairrosCidades = new List<Bairro>()
                                        {
                                                new Bairro() {
                                                Nome = "Piratininga",
                                                Ativo = true,
                                                CidadeId = 5
                                            },
                                                new Bairro() {
                                                Nome = "Bandeirantes",
                                                Ativo = true,
                                                CidadeId = 6
                                            }
                                        }
                                    },

                            }
            }

            }.ForEach(w => context.Estados.Add(w));



            new List<TipoImovel>() {

                new TipoImovel() {
                     Nome = "Casa",
                 //   Ativo = true
                },
                 new TipoImovel() {
                     Nome = "Ponto Comercial",
                   //  Ativo = true
                },
                  new TipoImovel() {
                     Nome = "Salas",
                  //   Ativo = true
                },
                   new TipoImovel() {
                     Nome = "Apartamentos",
                //     Ativo = true
                },
                    new TipoImovel() {
                     Nome = "Chácaras / Sitios",
                 //    Ativo = true
                },
                     new TipoImovel() {
                     Nome = "Galpões",
                 //    Ativo = true
                },
                      new TipoImovel() {
                     Nome = "Flat",
                  //   Ativo = true
                },
                       new TipoImovel() {
                     Nome = "Terrenos",
                    // Ativo = true
                },
                        new TipoImovel() {
                     Nome = "Condominios",
                    // Ativo = true
                },
                         new TipoImovel() {
                     Nome = "Chales",
                   //  Ativo = true
                },
            }.ForEach(x => context.TipoImoveis.Add(x));



            Usuario u = new Usuario()
            {
                Nome = "Agles da Silva",
                Ativo = true,
                Cpf = "225.327.038-57",
                DataCriacao = DateTime.Now,
                Email = "agles@usa.com.br",
                Senha = "AI2gjCCuJ0vP7DIftiq9YoZkQOoIFcLf7ldi6mBMlXCTLB6kGjw9w0JasV1i0MP70w==",
                UserName = "master",


            };
            context.Usuarios.Add(u);

            base.Seed(context);
        }
    }
}