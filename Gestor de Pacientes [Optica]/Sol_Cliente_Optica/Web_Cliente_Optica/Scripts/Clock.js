/**
 * @author Alexandros Estrella de la Mañana <alejandro.gh98@outlook.com>
 * @link https://github.com/alexandro-morningstar GitHub
 * ________________________________________________________________________________
 *|                                                                                |
 *|   JS para el comportamiento del reloj digital y matriz de puntos decorativos.  |
 *| _______________________________________________________________________________|
 */

// --------------- Definición de funciones ---------------

/**
 * Crea una matriz de 18 puntos (6x3)
 * @date 2024-11-1
 */
function createDots() {
    const matrizElement = document.getElementById('matriz');

    for (let i = 0; i < 18; i++) {
        const dot = document.createElement('div');
        dot.classList.add('punto');
        matrizElement.appendChild(dot); //Agrega un nodo sevundario al nodo primario (actual), en este caso un div
    };
};

/**
 * Con una instancia de Date se modifica el elemento "reloj".
 * Se actualiza cada segundo al llamarse con setInterval (al final del documento).
 * @date 2024-11-1
 */
function updateClock() {
    const clockElement = document.getElementById('reloj');
    const date = new Date();
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    const seconds = String(date.getSeconds()).padStart(2, '0');
    clockElement.textContent = `${hours}:${minutes}:${seconds}`;
};

/**
 * Define que punto se iluminará con un indice aleatorio modificando las propiedades de los objetos con clase .punto.
 * Se actualiza cada segundo al llamarse con setInterval (al final del documento).
 * @date 2024-11-1
 */
function lightDots() {
    const dots = document.querySelectorAll('.punto');
    dots.forEach(dot => dot.classList.remove('active')); // Apagar todos los puntos

    const randomIndex = Math.floor(Math.random() * dots.length);
    dots[randomIndex].classList.add('active'); // Encender un punto aleatorio
};

// --------------- Llamada de funciones ---------------
createDots();

setInterval(() => {
    updateClock();
    lightDots();
}, 1000);