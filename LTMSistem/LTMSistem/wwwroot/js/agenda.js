var dt = new Date();

var monthsNum = ["01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12"];

var months = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];

function renderDate() {
    // Faz a calendario funcionar
    dt.setDate(1);

    var day = dt.getDay();
    var endDate = new Date(dt.getFullYear(), dt.getMonth() + 1, 0).getDate();
    var prevDate = new Date(dt.getFullYear(), dt.getMonth(), 0).getDate();
    var hoje = new Date();
    var cells = "";
    var finalWeekDay = 0;
    var numDay = 1;
    var dataRota = window.location.href.slice(-10);

    for (x = day; x > 0; x--) {
        // Deixa os dias do mes anterior inativos
        cells += "<div class='mes-anterior' style='color: #918f9b;'>" + (prevDate - x + 1) + "</div>";
    }

    for (i = 1; i <= endDate; i++) {
        // Coloca os dias em ordem e marca o dia de hoje
        dataa = new Date(dt.getFullYear(), dt.getMonth(), i);

        if (i < 10) {
            // Se o dia for composto po um digito ele adicionara um 0 na frente
            if (i == hoje.getDate() && dt.getMonth() == hoje.getMonth()) {
                cells += "<div class='hoje' onClick='mostrarAgenda(\"" + dataa.toUTCString() + "\")'>" +
                    "<a role='button' href='/Agenda/MostraAgenda?data=" + dt.getFullYear() + "/" + monthsNum[dt.getMonth()] + "/" + 0 + i + "' style='text-decoration: none; color: white'>" + i + "</a>" + "</div>";
            }

            else {

                if ((dt.getFullYear() + "/" + monthsNum[dt.getMonth()] + "/" + 0 + i) == dataRota) {

                    cells += "<div class='selecionado' onClick='mostrarAgenda(\"" + dataa.toUTCString() + "\")'>" +
                        "<a role='button' href='/Agenda/MostraAgenda?data=" + dt.getFullYear() + "/" + monthsNum[dt.getMonth()] + "/" + 0 + i + "' style='text-decoration: none; color: white'>" + i + "</a>" + "</div>";



                }else {




                    cells += "<div onClick='mostrarAgenda(\"" + dataa.toUTCString() + "\")'>" +
                        "<a role='button' href='/Agenda/MostraAgenda?data=" + dt.getFullYear() + "/" + monthsNum[dt.getMonth()] + "/" + 0 + i + "' style='text-decoration: none; color: white'>" + i + "</a>" + "</div>";

                }
            }



        } else {
            if (i == hoje.getDate() && dt.getMonth() == hoje.getMonth()) {
                cells += "<div class='hoje' onClick='mostrarAgenda(\"" + dataa.toUTCString() + "\")'>" +
                    "<a role='button' href='/Agenda/MostraAgenda?data=" + dt.getFullYear() + "/" + monthsNum[dt.getMonth()] + "/" + i + "' style='text-decoration: none; color: white'>" + i + "</a>" + "</div>";
            } else {

                if ((dt.getFullYear() + "/" + monthsNum[dt.getMonth()] + "/"  + i) == dataRota) {

                    cells += "<div class='selecionado' onClick='mostrarAgenda(\"" + dataa.toUTCString() + "\")'>" +
                        "<a role='button' href='/Agenda/MostraAgenda?data=" + dt.getFullYear() + "/" + monthsNum[dt.getMonth()] + "/" + i + "' style='text-decoration: none; color: white'>" + i + "</a>" + "</div>";



                } else {

                    cells += "<div onClick='mostrarAgenda(\"" + dataa.toUTCString() + "\")'>" +
                        "<a role='button' href='/Agenda/MostraAgenda?data=" + dt.getFullYear() + "/" + monthsNum[dt.getMonth()] + "/" + i + "' style='text-decoration: none; color: white'>" + i + "</a>" + "</div>";
                }
            }


        }

        finalWeekDay = new Date(dt.getFullYear(), dt.getMonth(), i).getDay();
    }

    for (x = finalWeekDay + 1; x < 7; x++) {
        // Deixa os dias do proximo mes inativos
        cells += "<div style='color: #918f9b;' class='proximo-mes'>" + 0 + numDay + "</div>";
        numDay++;
    }

    mostrarAgenda(new Date().toUTCString());

    document.getElementById("date_str").innerHTML = dt.getFullYear();
    document.getElementById("month").innerHTML = months[dt.getMonth()];
    document.getElementsByClassName("dias")[0].innerHTML = cells;

   
}
var httpRequest;

function verInfo() {
    if (httpRequest.readyState === 4) {
        if (httpRequest.status === 200) {
            alert(httpRequest.responseText);
        } else {
            alert('There was a problem with the request.');
        }
    }
}

function request() {

    if (window.XMLHttpRequest) { // Mozilla, Safari, ...
        httpRequest = new XMLHttpRequest();
    } else if (window.ActiveXObject) { // IE 8 and older
        httpRequest = new ActiveXObject("Microsoft.XMLHTTP");
    }

    httpRequest.onreadystatechange = verInfo;
    httpRequest.open('GET', 'http://localhost/TCC2/js/data.txt.txt', true);
    // httpRequest.send(null);
}



function mostrarAgenda(a) {
    // Funçãode poder clicar no dia e mostrar a agenda

    var a2 = new Date(a);
    var cellsHoje = " ";
    // if (getDate() > 9) {
    cellsHoje += "<h5> Agendamento de " + a2.getDate() + " de " + months[a2.getMonth()] + " de " + a2.getFullYear() + ":</h5>";
    document.getElementsByClassName("aHoje")[0].innerHTML = cellsHoje;
    request("data=" + cellsHoje);

}

function moveDate(para) {
    // Botão que move entre o mes anteriore o proximo mes

    if (para == 'anterior') {
        dt.setMonth(dt.getMonth() - 1);
    } else if (para == 'proximo') {
        dt.setMonth(dt.getMonth() + 1);
    }
    renderDate();
}


//"<a href='consulta/MostraAgenda/" + dt.getFullYear() + monthsNum[dt.getMonth()] + i + "' style='text-decoration: none; color: white'>" + i + "</a>" + "</div>";
//body "<a href='consulta/" + dt.getFullYear() + monthsNum[dt.getMonth()] + i + "' style='text-decoration: none; color: white'>" + i + "</a>" + "</div>";



//if ((dt.getFullYear() + "/" + monthsNum[dt.getMonth()] + "/" + 0 + i) == window.location.href.slice(-10)) {

//    cells += "<div class='bg-white' onClick='mostrarAgenda(\"" + dataa.toUTCString() + "\")'>" +
//        "<a role='button' href='/Consulta/MostraAgenda?data=" + dt.getFullYear() + "/" + monthsNum[dt.getMonth()] + "/" + 0 + i + "' style='text-decoration: none; color: white'>" + i + "</a>" + "</div>";
//} 