requirejs.config({
    "baseUrl": "scripts",
    "paths": {
        app: "/app",
        jquery: "jquery-2.1.4",
        react: "https://cdnjs.cloudflare.com/ajax/libs/react/0.13.3/react",
        JSXTransformer: "JSXTransformer"
        //reacttransformer: "https://cdnjs.cloudflare.com/ajax/libs/react/0.13.3/JSXTransformer"
    }
});

requirejs(["jsx!main"]);