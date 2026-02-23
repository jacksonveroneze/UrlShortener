\set rid random(1, :maxid)

SELECT u.code, u.created_at, u.deleted_at, u.expires_at, u.original_url, u.updated_at, u.version
FROM shortener.url AS u
JOIN shortener.test_ids AS t
  ON t.code = u.code
WHERE u.deleted_at IS NULL
  AND t.id = :rid
LIMIT 1;