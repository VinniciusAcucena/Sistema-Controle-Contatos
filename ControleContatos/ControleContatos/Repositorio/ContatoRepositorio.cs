using ControleContatos.Data;
using ControleContatos.Models;

namespace ControleContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancocontext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            this._bancocontext = bancoContext;
        }

        public List<ContatoModel> BuscarTodos()
        {
            return _bancocontext.Contatos.ToList();
        }

        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancocontext.Contatos.Add(contato);
            _bancocontext.SaveChanges();
            return contato;
        }

        public ContatoModel ListarPorId(int id)
        {
            return _bancocontext.Contatos.FirstOrDefault(x => x.Id == id);

        }

        public ContatoModel Alterar(ContatoModel contato)
        {
            var DBcontato = ListarPorId(contato.Id);

            if (DBcontato == null) throw new Exception("Houve um erro na alteração do contato");

            DBcontato.Nome = contato.Nome;
            DBcontato.Email = contato.Email;
            DBcontato.Celular = contato.Celular;

            _bancocontext.Contatos.Update(DBcontato);
            _bancocontext.SaveChanges();

            return DBcontato;
        }

        public bool Apagar(int id)
        {
            ContatoModel DBcontato = ListarPorId(id);

            if (DBcontato == null) throw new Exception("Houve um erro na deleção do contato");

            _bancocontext.Contatos.Remove(DBcontato);
            _bancocontext.SaveChanges();

            return true;
        }
    }
}
