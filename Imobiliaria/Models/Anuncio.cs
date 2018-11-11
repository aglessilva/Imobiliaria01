using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PagedList;
using System.Linq;

namespace Imobiliaria.Models
{

    public class Anuncio
    {
        public Anuncio()
        {
            this.Imagens = new HashSet<ImagensAnuncio>();
            this.Enderecos = new HashSet<Endereco>();
        }

       
        public int Id { get; set; }
        public bool Ativo { get; set; }

        [Required(ErrorMessage ="Campo Obrigatorio")]
        [Display(Name ="Negócio")]
        public int TipoAnuncio { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        public int Tipo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        public int Dorms { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        public int Suite { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        public int Vagas { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [StringLength(10,ErrorMessage = "Máximo de 3 a 10 caracteres", MinimumLength = 3)]
        public string Area { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Digite somente números")]
        [Range(0, int.MaxValue, ErrorMessage = "não é permitido valores menor que zero")]
        public decimal Valor { get; set; }

        public DateTime DataAnuncio { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [StringLength(100, ErrorMessage = "Máximo de 5 a 100 caracteres", MinimumLength = 5)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatorio")]
        [StringLength(500, ErrorMessage = "Máximo de 50 a 500 caracteres", MinimumLength = 50)]
        [Display(Name = "Descição")]
        public string Descricao { get; set; }

        [Display(Name = "Lançamento")]
        public bool IsLancamento { get; set; }

        public virtual ICollection<ImagensAnuncio> Imagens{ get; set; }
        public virtual ICollection<Endereco> Enderecos { get; set; }


        public List<Anuncio> FindAll(string _referencia = null)
        {
            DBConn db = new DBConn();
            int id = String.IsNullOrWhiteSpace(_referencia) ? 0 : Convert.ToInt32(_referencia);
            return db.Anuncios.Where(x => x.Id == (id == 0 ? x.Id : id) && x.Ativo == true).OrderByDescending(x => x.IsLancamento).ThenBy(x => Guid.NewGuid()).ToList();
        }

        public List<Anuncio> Filter(int[] _array)
        {
            int _tipo = _array[0], _estado = _array[1], _bairro = _array[2], _cidade = _array[3], _valorInicial = _array[4], _valorFinal = _array[5], _tipoAnuncio = _array[6] ;

            try
            {
                DBConn db = new DBConn();
                var ret = db.Enderecos.Join
                    (
                    db.Anuncios, p => p.AnundioId, an => an.Id, (p, an) => new { p = p, an = an }
                    ).Where
                    (x => (
                        (x.an.Tipo == _tipo)
                        && (x.an.Ativo == true)
                        && (x.an.TipoAnuncio == (_tipoAnuncio == 0 ? x.an.TipoAnuncio : _tipoAnuncio))
                        && (x.p.EstadoId == (_estado == 0 ? x.p.EstadoId : _estado))
                        && (x.p.CidadeId == (_cidade == 0 ? x.p.CidadeId : _cidade))
                        && (x.p.BairroId == (_bairro == 0 ? x.p.BairroId : _bairro))
                        && (x.an.Valor >= (_valorInicial == 0 ? 0 : _valorInicial) && x.an.Valor <= (_valorFinal == 0 ? 90000000000 : _valorFinal))
                        )
                     ).OrderBy(x => x.an.Valor)
                  .Select(
                     w =>
                        new
                        {
                            Id = w.p.AnundioId,
                            Titulo = w.an.Titulo,
                            Descricao = w.an.Descricao,
                            Suite = w.an.Suite,
                            Vagas = w.an.Vagas,
                            Valor = w.an.Valor,
                            Dorms = w.an.Dorms,
                            Area = w.an.Area,
                            Imagens = w.an.Imagens
                        }
                  ).ToList();

                List<Anuncio> _list = new List<Anuncio>();
                ret.ForEach(c => {
                    var item = new Anuncio() {
                       Id = c.Id,
                       Area = c.Area,
                       Descricao = c.Descricao,
                       Dorms = c.Dorms,
                       Titulo = c.Titulo,
                       Suite = c.Suite,
                       Vagas = c.Vagas,
                       Valor = c.Valor,
                       Imagens = c.Imagens
                    };

                    _list.Add(item);
                });
                return  _list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
        }

    }

}