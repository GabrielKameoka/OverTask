/** @type {import('tailwindcss').Config} */
module.exports = {
    darkMode: 'class', // Usa a classe 'dark' para alternar modo escuro
    content: [
        "./**/*.html",
        "./**/*.razor",
        "./**/*.cshtml",
        "./**/*.js",
        "./**/*.ts"
    ],

    theme: {
        extend: {
            colors: {
                base: {
                    light: "#CCD0CF", // fundo claro
                    dark: "#11212D",  // fundo escuro
                },
                text: {
                    light: "#11212D", // texto claro
                    dark: "#CCD0CF",  // texto escuro
                },
                secondary: "#4A5C6A", // bordas, detalhes
            }
        }
    },
    plugins: [],
}
