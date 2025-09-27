Use 5to_MarketWeight

CALL AltaCriptoMoneda(100.50, 100, 'Bitcoin', 'https://s2.coinmarketcap.com/static/img/coins/64x64/1.png');
CALL AltaCriptoMoneda(50.25, 100, 'Ethereum', 'https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png');
CALL AltaCriptoMoneda(75.00, 100, 'Ripple', 'https://s2.coinmarketcap.com/static/img/coins/64x64/52.png');
CALL AltaCriptoMoneda(20.75, 100, 'Litecoin', 'https://s2.coinmarketcap.com/static/img/coins/64x64/2.png');
CALL AltaCriptoMoneda(30.10, 100, 'Cardano', 'https://s2.coinmarketcap.com/static/img/coins/64x64/2010.png');
CALL AltaCriptoMoneda(60.80, 100, 'Polkadot', 'https://s2.coinmarketcap.com/static/img/coins/64x64/6636.png');
CALL AltaCriptoMoneda(90.00, 100, 'Chainlink', 'https://s2.coinmarketcap.com/static/img/coins/64x64/1975.png');
CALL AltaCriptoMoneda(110.15, 100, 'Stellar', 'https://s2.coinmarketcap.com/static/img/coins/64x64/512.png');
CALL AltaCriptoMoneda(40.60, 100, 'Dogecoin', 'https://s2.coinmarketcap.com/static/img/coins/64x64/74.png');
CALL AltaCriptoMoneda(70.85, 100, 'Tron', 'https://s2.coinmarketcap.com/static/img/coins/64x64/1958.png');

CALL AltaUsuario('Ana', 'García', 'ana.garcia@example.com', 'pass1234', 0);
CALL AltaUsuario('Luis', 'Martínez', 'luis.martinez@example.com', '1234abcd', 0);
CALL AltaUsuario('Marta', 'Fernández', 'marta.fernandez@example.com', 'abcd1234', 0);
CALL AltaUsuario('Carlos', 'Gómez', 'carlos.gomez@example.com', 'qwerty12', 0);
CALL AltaUsuario('Laura', 'Rodríguez', 'laura.rodriguez@example.com', 'password', 0);
CALL AltaUsuario('Pedro', 'López', 'pedro.lopez@example.com', 'abcd1234', 0);
CALL AltaUsuario('Sofía', 'Hernández', 'sofia.hernandez@example.com', '12345678', 0);
CALL AltaUsuario('Daniel', 'Pérez', 'daniel.perez@example.com', '1q2w3e4r', 0);
CALL AltaUsuario('María', 'Torres', 'maria.torres@example.com', 'letmein1', 0);
CALL AltaUsuario('Javier', 'Ramírez', 'javier.ramirez@example.com', 'welcome1', 0);
CALL IngresarDinero(2, 0);
CALL IngresarDinero(3, 10000);
CALL IngresarDinero(2, 10000);

CALL ComprarMoneda (2, 3, 2);
CALL Transferencia (2, 0.5, 2, 3);