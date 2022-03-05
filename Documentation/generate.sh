#!/bin/bash
#
# Auto generate documentation and push to `docs` branch
#
git branch -D docs;

doxygen Documentation/Doxyfile;
mv ~Docs docs;

git add .;
git checkout -b docs;
git commit -m "Generated documentation";
git push origin docs -f;

mv docs ~Docs;
git checkout master;
