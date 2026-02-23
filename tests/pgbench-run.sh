#!/usr/bin/bash

echo "-> Start" && date

#-n “No vacuum / no init”. Não tenta fazer VACUUM/initialize. Útil quando você só quer rodar o teste no banco já pronto.
#-M Modo de execução das queries: simple/simple/extended
#-D maxid=38000 Define uma variável maxid no script.
#-c 180 Número de clientes simultâneos = número de conexões concorrentes do pgbench para o alvo
#-j 10 Número de threads do pgbench gerando carga.
#-T 60 Duração do teste em segundos (aqui: 60s).
#-P 5 Mostra progresso a cada 5s (TPS, latência, falhas).

pgbench \
  -n \
  -M prepared \
  -f read_url.sql \
  -D maxid=38000 \
  -c 20 \
  -j 5 \
  -T 10 \
  -P 1 \
  "host=10.0.0.150 port=6432 dbname=url_shortener user=postgres password=localP@ssword"

echo "---------------"

pgbench \
  -n \
  -M prepared \
  -f read_url.sql \
  -D maxid=38000 \
  -c 180 \
  -j 10 \
  -T 60 \
  -P 5 \
  "host=10.0.0.150 port=6432 dbname=url_shortener user=postgres password=localP@ssword"

echo "-> end" && date