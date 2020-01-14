$(document).ready(function () {
    $('.textarea-editor').summernote(
        {
          //  fontSizes: ['8', '9', '10', '11', '12', '14', '18','20','22','24','26','28','38','46','72'],
            height: 300, // define a altura do editor
            minHeight: null, // define a altura minima
            maxHeight: null, // define a altura máxima
            focus: true, // define o foco na área editável apos a inicialização

                               // height: 200,
          //  tabsize: 6,
           // placeholder: "Type here...",
            dialogsFade: true,
            toolbar: [
                ['style', ['style']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['font', ['bold', 'italic', 'underline', 'clear']],
                ['fontsize', ['fontsize']],
                ['fontname', ['fontname']],
                ['color', ['color']],
                ['para', ['ul', 'ol', 'paragraph']],
                ['height', ['height']]
                ['table', ['table']],
                ['insert', ['link', 'picture', 'video']],
                ['view', ['fullscreen', 'codeview', 'help']]
            ]

        });
});