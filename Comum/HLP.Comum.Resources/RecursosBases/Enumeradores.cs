using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace HLP.Comum.Resources.RecursosBases
{
    public enum OperacaoCadastro
    {
        livre,
        criando,
        alterando,
        pesquisando
    }

    public enum statusModel
    {
        nenhum,
        criado,
        alterado,
        excluido
    }

    #region Enum to DataGrid

    public enum SEXO { FEMININO, MASCULINO };

    public enum TipoEndereco
    {
        [Description("0-COMERCIAL")]
        COMERCIAL,
        [Description("1-ENTREGA")]
        ENTREGA,
        [Description("2-ENTREGA ALT")]
        ENTREGA_ALT,
        [Description("3-NOTA FISCAL")]
        NOTA_FISCAL,
        [Description("4-RESIDÊNCIA")]
        RESIDÊNCIA,
        [Description("5-SERVIÇO")]
        SERVICO,
        [Description("6-SWIFT")]
        SWIFT,
        [Description("7-PAGAMENTO")]
        PAGAMENTO,
        [Description("9-OUTRO")]
        OUTRO
    };

    public enum stOrigem
    {
        [Description("0 - Nacional")]
        NACION,
        [Description("1 - Estrangeira – Importação Direta")]
        ESTRIMPDIRET,
        [Description("2 - Estrangeira – Adquirida no mercado Interno.")]
        ESTRMERCINTERNO,
        [Description("3 - Nacional, mercadoria ou bem com Conteúdo de Importação superior a 40% (quarenta por cento)")]
        NACIONBEMIMPORTSUP40,
        [Description("4 - Nacional, cuja produção tenha sido feita em conformidade com os processos produtivos básicos de que tratam o Decreto-Lei nº 288/67, e as Leis nºs 8.248/91, 8.387/91, 10.176/01 e 11 . 4 8 4 / 0 7")]
        NACIONDECRETO28867,
        [Description("5 - Nacional, mercadoria ou bem com Conteúdo de Importação inferior ou igual a 40% (quarenta por cento)")]
        NACIONBEMIMPORTINF40,
        [Description("6 - Estrangeira - Importação direta, sem similar nacional, constante em lista de Resolução CAMEX")]
        ESTRIMPDIRETSEMSIMILNACION,
        [Description("7 - Estrangeira - Adquirida no mercado interno, sem similar nacional, constante em lista de Resolução CAMEX”")]
        ESTRMERCINTERNOSEMSIMILNACION
    }

    public enum TipoLogradouro
    {
        [Description("00-AER - AEROPORTO")]
        AEROPORTO,
        [Description("01-AL - ALAMEDA")]
        ALAMEDA,
        [Description("02-AP - APARTAMENTO")]
        APARTAMENTO,
        [Description("03-AV - AVENIDA")]
        AVENIDA,
        [Description("04-BC - BECO")]
        BECO,
        [Description("05-BL - BLOCO")]
        BLOCO,
        [Description("06-CAM - CAMINHO")]
        CAMINHO,
        [Description("07-ESCD - ESCADINHA")]
        ESCADINHA,
        [Description("08-EST  - ESTAÇÃO")]
        ESTAÇÃO,
        [Description("09-ETR - ESTRADA")]
        ESTRADA,
        [Description("10-FAZ - FAZENDA")]
        FAZENDA,
        [Description("11-FORT - FORTALEZA")]
        FORTALEZA,
        [Description("12-GL - GALERIA")]
        GALERIA,
        [Description("13-LD - LADEIRA")]
        LADEIRA,
        [Description("14-LGO  - LARGO")]
        LARGO,
        [Description("15-PÇA - PRAÇA")]
        PRAÇA,
        [Description("16-PR  - PRAIA")]
        PRAIA,
        [Description("17-PRQ - PARQUE")]
        PARQUE,
        [Description("18-QD  - QUADRA")]
        QUADRA,
        [Description("19-KM - QUILÔMETRO")]
        QUILÔMETRO,
        [Description("20-QTA  - QUINTA")]
        QUINTA,
        [Description("21-ROD  - RODOVIA")]
        RODOVIA,
        [Description("22-R - RUA")]
        RUA,
        [Description("23-SQD - SUPER QUADRA")]
        SUPER_QUADRA,
        [Description("24-TRV - TRAVESSA")]
        TRAVESSA,
    }

    public enum SemanaOuMes
    {
        [Description("0 - SEMANA")]
        SEMANA,
        [Description("1 - MÊS")]
        MES
    }

    public enum DiaUtil
    {
        [Description("0-NÃO SE APLICA")]
        NAO_SE_APLICA,
        [Description("1-SEGUNDA")]
        SEGUNDA,
        [Description("2-TERÇA")]
        TERCA,
        [Description("3-QUARTA")]
        QUARTA,
        [Description("4-QUINTA")]
        QUINTA,
        [Description("5-SEXTA")]
        SEXTA
    }

    public enum TipoCertificacao
    {
        [Description("0-ENSINO MÉDIO")]
        ENSINOMEDIO,
        [Description("1-ENSINO SUPERIOR")]
        ENSINOSUPERIOR,
        [Description("2-MBA/PÓS-GRADUAÇÃO")]
        MBAPOSGRADUACAO,
        [Description("3-MESTRADO")]
        MESTRADO,
        [Description("4-DOUTORADO")]
        DOUTORADO,
        [Description("5-PÓS-DOUTORADO")]
        POSDOUTORADO,
        [Description("6-CURSOS COMPLEMENTARES")]
        CURSOSCOMPLEMENTARES,
        [Description("7-OUTROS")]
        OUTROS
    }

    public enum TipoComissao
    {
        [Description("0-Por Representante (Default)")]
        REPRESENTANTE,
        [Description("1-Por Lista de Preço")]
        LISTAPREDEPRECO,
        [Description("2-Por Família de Produto")]
        FAMILIAPRODUTO,
        [Description("3-Por Produto")]
        PRODUTO,
        [Description("4-Por Faixa Margem de Venda")]
        FAIXAMARGEMVENDA
    }

    public enum TipoArredondamento
    {
        [Description("0-Para Baixo")]
        PARABAIXO,
        [Description("1-Para Cima")]
        PARACIMA,
    }

    public enum ValorOuPorcentagem
    {
        [Description("0-PORCENTAGEM")]
        PORCENTAGEM,
        [Description("1-VALOR ESPECÍFICO")]
        VALOR
    }

    #endregion
}

