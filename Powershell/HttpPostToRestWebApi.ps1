$postParams = @{username='me';moredata='qwerty'}
Invoke-WebRequest -Uri http://example.com/foobar -Method POST -Body $postParams