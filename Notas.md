# Notas


## OData

### Limitaciones actuales.

Pese a toda la flexibilidad que ofrece OData, existen ciertas limitaciones al interactuar con Stored Procedures.
Los stored procedures no permiten aprovechar las ventajas de OData, como hacer filtrado, ordenamiento o paginaci�n directamente en la consulta. 
Esto se debe a que OData est� dise�ado para trabajar con entidades y relaciones, mientras que los stored procedures son procedimientos predefinidos 
que pueden contener l�gica compleja y no necesariamente siguen el modelo de datos de OData.
El resultado es que todo termina aplic�ndose en memoria, lo que puede afectar el rendimiento, especialmente con grandes conjuntos de datos.
Por ahora utilizaremos esto, pero es recomendable, por lo pronto, forzar la mayor cantidad de par�metros posibles para que
el resultado sea lo m�s filtrado posible, y as� minimizar el impacto en memoria.
Posteriormente, ser� necesario evaluar migrar de SP a otro formato, sean TVF, Vistas, o directamente a c�digo, para aprovechar al m�ximo las ventajas de OData.


## Pendientes

* Agregar mecanismos de loggeo.
* Comenzar a agregar pruebas unitarias.
* Cómo reorganizo fyd.backend.Identidad?
* Refresh token
* Tema permisos: Ver como emparejar con lo que est� en SrvGA.Sincronizaci�n entre bases no es urgente para nada y se puede patear para adelante.

## TODO
* MonedasTipoLey es un endpoint
* TipoCuentaLey es un endpoint
* Agregar CancellationToken para operaciones de lectura de sp.
* El parámetro de cantidad de registros retornados por OData deberá ser customizable.

## TODO: Módulo de Asientos

### Servicios

* **Renumeración de asientos** falta implementar esta funcionalidad en el controlador, en la capa de servicios, etc.


### Infraestructura / Entidades Faltantes

* **Entidad `Caja` (`fdsCajas`)**: No existe en el dominio. Se necesita para validaciones de `Asiento` y su correspondiente `AsientoRepositorio`.
* **Entidad `Periodo` / `Consolidación`**: Implementar lógica para identificar períodos contablemente cerrados (VB6: `PeriodoCerradoOK`).
* **Tabla `ctbAsientosComprobantes`**: Join table necesaria para validar si un asiento tiene un "comprobante padre" (Factura, Cobranza, etc.) y así bloquear su edición/borrado.
* **Tabla `gnrVinculaciones`**: Necesaria para validar si el asiento está vinculado por el programa aplicador.
* **Tabla `gnrAplicaciones`**: Necesaria para validar si el comprobante está aplicado en Cta. Cte. de Ventas o Compras.
* **Módulo de Numeración**: Implementar `gnrNumeraTipos` y `gnrNumeraciones` para manejar letras, puntos de venta y auto-numeración de comprobantes.

### Mejoras / Consultas

* Completar `ConsultarAsientoDto` con nombres de Empresa, Moneda, Cuenta y Subcuentas (Cliente/Proveedor/Caja) una vez que las entidades de referencia existan en el sistema.
* Implementar auto-numeración en `AsientoServicio` si el número de comprobante es nulo.
* Completar validaciones de integridad en `AsientoRepositorio` una vez que las tablas legacy mencionadas arriba estén mapeadas.